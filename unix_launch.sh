#!/bin/bash
max_jobs=32
set +x

run()
{
	login
	fetchinventory
	updatelibs "${libs[@]}"
	mkdirs ${asset_dirs[@]}
	updateassets ${assets[@]}
	startclient 1.6.4
}

login()
{
	read -p "Enter your Huckleberry username: " un
	read -sp "Enter your Huckleberry password: " pn
	echo

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

updatelibs()
{
	output "Updating libraries"
	if [ ! -d "$lib_dir" ]; then
	  mkdir -p "$lib_dir"
	fi
	while [ $# -gt 0 ]; do
		running=$(jobs -pr)
		if [ ${#running[@]} -lt $max_jobs ]; then
			updatelib $1 &
			shift
		fi
	done
	wait
	echo
}

mkdirs()
{
	output "Creating asset folders"
	while [ $# -gt 0 ]; do
		output . "Ensuring $1 exists." 2
		running=$(jobs -pr)
		if [ ${#running[@]} -lt $max_jobs ]; then
			mkdir -p "${path}assets/$1" &
			shift
		fi
	done
	wait
	echo
}

updateassets()
{
	output "Updating asset files"
	while [ $# -gt 0 ]; do
		running=$(jobs -pr)
		if [ ${#running[@]} -lt $max_jobs ]; then
			updateasset $1 &
			shift
		fi
	done
	wait
	echo
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

verbose=0
path=$PWD/
while [ $# -gt 0 ]; do
	if [ "$1" == "-v" ]; then
		(( verbose=$verbose+1 ))
	elif [ "$1" == "-p" ]; then
		shift
		path=$1
	fi
	shift
done
lib_dir="${path}libs/"

run
