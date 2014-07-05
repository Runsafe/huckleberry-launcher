#!/bin/bash
set +x

run()
{
	login
	fetchinventory
	parallelize updatelib "Updating libraries" "${libs[@]}"
	parallelize mkassetdir "Creating asset folders" "${asset_dirs[@]}"
	parallelize updateasset "Updating asset files" "${assets[@]}"
	startclient 1.6.4
}

login()
{
	if [ -z "$un" ]; then
		read -p "Enter your Huckleberry username: " un
	fi
	if [ -z "$pn" ]; then
		read -sp "Enter your Huckleberry password: " pn
		echo
	fi

	UUID=$(curl -s "https://huckleberry.runsafe.no/auth.php?username=${un}&password=${pn}")

	if [ $UUID = 'invalid' ]; then
		echo "Invalid username and/or password"
		read -p "Press [Enter] to exit."
		exit
	fi

	if [ $UUID = 'noinvite' ]; then
		echo "You have not been invited to Huckleberry yet!"
		read -p "Press [Enter] to exit."
		exit
	fi

	key=${UUID:4}
	user=$(echo $key | awk -F"," '{print $1}')
	key=$(echo $key | awk -F"," '{print $2}')
}

fetchinventory()
{
	IFS="`echo -e '\x1F'`" read -a asset_data <<< "`curl -s "https://huckleberry.runsafe.no/assets.dat"`"
	IFS="`echo -e '\x1E'`" read -a asset_dirs <<< "${asset_data[0]}"
	IFS="`echo -e '\x1E'`" read -a assets <<< "${asset_data[1]}"
	IFS="," read -a libs <<< "`curl -s "https://huckleberry.runsafe.no/lib_list.php"`"
}

parallelize()
{
	command=$1
	shift
	output "$1"
	shift
	while [ $# -gt 0 ]; do
		running=$(jobs -pr)
		if [ ${#running[@]} -lt $max_jobs ]; then
			$command $1 &
			shift
		else
			wait
		fi
	done
	wait
	echo done!
	
}

startclient()
{
	classpath=$(join ':' libs/*.jar)
	java -Djava.library.path="libs" -cp "$classpath" net.minecraft.client.main.Main --username $user --session $key --version $1
}

updatelib()
{
	lib=$1
	lib_name=$(echo $lib | awk -F":" '{print $1}')
	lib_hash=$(echo $lib | awk -F":" '{print $2}')
	lib_path="$lib_dir$lib_name"
	url="https://huckleberry.runsafe.no/client/libs/$lib_name"
	updatefile "$url" "$lib_hash" "$lib_path"
}

mkassetdir()
{
	if [ ! -d "${path}assets/$1" ]; then
		output N "Creating folder $1"
		mkdir -p "${path}assets/$1"
	else
		output . "Skipping folder $1" 2
	fi
}

updateasset()
{
	asset=$1
	asset_name=$(echo $asset | awk -F":" '{print $1}')
	asset_hash=$(echo $asset | awk -F":" '{print $2}')
	asset_path="${path}assets/$asset_name"
	url="https://huckleberry.runsafe.no/client/assets/$asset_name"
	updatefile "$url" "$asset_hash" "$asset_path"
}

updatefile()
{
	sourceuri=$1
	remote_hash=$2
	local_hash=""
	target=$3
	if [ -f $target ]; then
		if [ ! -f "${target}.md5" -o "${target}.md5" -ot "${target}" ]; then
			output '#' "Rehashing local file ${target}"
			__rvm_md5_for "$target" > "${target}.md5"
		fi
		local_hash=$(cat "${target}.md5")
	fi
	if [ -n "$remote_hash" -a $remote_hash != "$local_hash" ]; then
		if [ -z "$local_hash" ]; then
			output N "Downloading new file from $sourceuri"
		else
			output U "Downloading modified file from $sourceuri"
		fi
		curl -s -o "$target" $sourceuri
		__rvm_md5_for "$target" > "${target}.md5"
		local_hash=$(cat "${target}.md5")
		if [ $remote_hash != "$local_hash" ]; then
			echo
			echo "Wrong hash downloading resource $sourceuri, aborting!"
			rm -f "$target"
			exit 1
		fi
	else
		output . "Skipping up to date file $sourceuri" 2
	fi
}

__rvm_md5_for()
{
  if builtin command -v md5 > /dev/null; then
    echo -n md5 $1
  elif builtin command -v md5sum > /dev/null ; then
    echo -n | md5sum $1 | awk '{print $1}'
  else
    return 1
  fi

  return 0
}

join()
{
	local IFS=$1
	shift
	echo "$*"
}

output()
{
	if [ $verbose -eq 0 ]; then
		echo -n $1
	elif [ -z "$3" ] || [ "$verbose" -ge "$3" ]; then
		if [ -z "$2" ]; then
			echo $1
		else
			echo $2
		fi
	fi
}

usage()
{
	echo "$0 [options]"
	echo " Available options:"
	echo "  -v                Modify output verbosity. Specify multiple times to increase level."
	echo "  -p <path>         Specify where to store assets. Defaults to current folder."
	echo "  -j <maxjobs>      Control paralellism of downloader."
	echo "  --username <user> Specify username to log in as from command line."
	echo "  --password <pass> Specify password to log in with from command line."
	echo "  --help            Show this help"
	exit 0
}

max_jobs=32
verbose=0
path=$PWD/
un=
pn=
while [ $# -gt 0 ]; do
	case $1 in
		"-v")		(( verbose=$verbose+1 ));;
		"-p")		shift; path=$1 ;;
		"--username")	shift; un=$1 ;;
		"--password")	shift; pn=$1 ;;
		"-j")		shift; max_jobs=$1 ;;
		"--help")	usage ;;
	esac
	shift
done
lib_dir="${path}libs/"
if [ ! -d "$lib_dir" ]; then
  mkdir -p "$lib_dir"
fi

run
