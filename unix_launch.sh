#!/bin/bash
set +x

run()
{
	echo -e '\033]2;Huckleberry\007'
	update $*
	getargs $*
	login
	mkdirs
	fetchinventory
	parallelize updatelib "${GREEN}Updating libraries${RESET}" "${libs[@]}"
	parallelize mkassetdir "${GREEN}Creating asset folders${RESET}" "${asset_dirs[@]}"
	parallelize updateasset "${GREEN}Updating asset files${RESET}" "${assets[@]}"
	cleanup 
	startclient 1.6.4
}

update()
{
	temp=$(mktemp)
	echo "Checking for launcher update"
	curl -s -o $temp "https://raw.githubusercontent.com/Runsafe/huckleberry-launcher/${channel}/unix_launch.sh"
	latest=$(__rvm_md5_for $temp)
	running=$(__rvm_md5_for $0)
	if [ "$latest" != "$running" ]; then
		echo "Updating launcher"
		mv $temp $0
		chmod +x $0
		echo "Restarting launcher"
		$0 $*
		exit
	else
		rm -f $temp
	fi
}

login()
{
	if [ -z "$un" ]; then
		read -p "`echo -e ${BRIGHT_CYAN}Enter your ${BRIGHT_GREEN}Huckleberry ${BRIGHT_CYAN}username:${YELLOW}`" un
	fi
	if [ -z "$pn" ]; then
		read -sp "`echo -e ${BRIGHT_CYAN}Enter your ${BRIGHT_GREEN}Huckleberry ${BRIGHT_CYAN}password:`" pn
		echo
	fi
	echo -ne $RESET

	UUID=$(curl -s "https://huckleberry.runsafe.no/auth.php?username=${un}&password=${pn}")

	if [ $UUID = 'invalid' ]; then
		echo -e "${REV_RED}Invalid username and/or password${RESET}"
		read -p "`echo -e Press ${CYAN}[Enter]${RESET} to exit.`"
		exit
	fi

	if [ $UUID = 'noinvite' ]; then
		echo -e "${REV_RED}You have not been invited to Huckleberry yet!${RESET}"
		read -p "`echo -e Press ${CYAN}[Enter]${RESET} to exit.`"
		exit
	fi

	key=${UUID:4}
	user=$(echo $key | awk -F"," '{print $1}')
	key=$(echo $key | awk -F"," '{print $2}')
}

fetchinventory()
{
	local IFS=`echo -e '\x1F'`
	read -a asset_data <<< "`curl -s "https://huckleberry.runsafe.no/assets.dat"`"
	local IFS=`echo -e '\x1E'`
	read -a asset_dirs <<< "${asset_data[0]}"
	read -a assets <<< "${asset_data[1]}"
	local IFS=","
	read -a libs <<< "`curl -s "https://huckleberry.runsafe.no/lib_list.php"`"
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
	echo -e "${BRIGHT_GREEN}done!${RESET}"
	
}

cleanup()
{
	find $asset_dir $lib_dir -type f -mmin +30 -delete
	find -type d -empty -delete
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
	if [ ! -d "${asset_dir}$1" ]; then
		output "${BRIGHT_GREEN}N${RESET}" "Creating folder: ${BRIGHT_GREEN}$1${RESET}"
		mkdir -p "${asset_dir}$1"
	else
		output . "Skipping folder: ${YELLOW}$1${RESET}" 2
	fi
}

updateasset()
{
	asset=$1
	asset_name=$(echo $asset | awk -F":" '{print $1}')
	asset_hash=$(echo $asset | awk -F":" '{print $2}')
	asset_path="${asset_dir}$asset_name"
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
			output "${BRIGHT_RED}#${RESET}" "Rehashing local file ${BRIGHT_RED}${target}${RESET}"
			__rvm_md5_for "$target" > "${target}.md5"
		fi
		local_hash=$(cat "${target}.md5")
	fi
	if [ -n "$remote_hash" -a $remote_hash != "$local_hash" ]; then
		if [ -z "$local_hash" ]; then
			output "${BRIGHT_GREEN}N${RESET}" "Downloading new file from ${BRIGHT_GREEN}$sourceuri${RESET}"
		else
			output "${YELLOW}U${RESET}" "Downloading modified file from ${YELLOW}$sourceuri${RESET}"
		fi
		curl -s -o "$target" $sourceuri
		__rvm_md5_for "$target" > "${target}.md5"
		local_hash=$(cat "${target}.md5")
		if [ $remote_hash != "$local_hash" ]; then
			echo
			echo "${REV_RED}Wrong hash downloading resource $sourceuri, aborting!${RESET}"
			rm -f "$target"
			exit 1
		fi
	else
		output . "Skipping up to date file ${BRIGHT_BLACK}$sourceuri${RESET}" 2
		touch "${target}" "${target}.md5"
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
		echo -ne $1
	elif [ -z "$3" ] || [ "$verbose" -ge "$3" ]; then
		if [ -z "$2" ]; then
			echo -e $1
		else
			echo -e $2
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
	echo "  --channel <name>  Use a different update channel for launcher."
	echo "  --help            Show this help"
	exit 0
}

getargs()
{
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
}

mkdirs()
{
	lib_dir="${path}libs/"
	asset_dir="${path}assets/"
	if [ ! -d "$lib_dir" ]; then
		mkdir -p "$lib_dir"
	fi
	if [ ! -d "$asset_dir" ]; then
		mkdir -p "$asset_dir"
	fi
}

channel=unix-latest
max_jobs=32
verbose=0
path=$PWD/
un=
pn=
DULL=0
BRIGHT=1
FG_BLACK=30
FG_RED=31
FG_GREEN=32
FG_YELLOW=33
FG_BLUE=34
FG_VIOLET=35
FG_CYAN=36
FG_WHITE=37
FG_NULL=00
BG_BLACK=40
BG_RED=41
BG_GREEN=42
BG_YELLOW=43
BG_BLUE=44
BG_VIOLET=45
BG_CYAN=46
BG_WHITE=47
BG_NULL=00
ESC="\001\e"
NORMAL="$ESC[m\002"
RESET="$ESC[${DULL};${FG_WHITE};${BG_NULL}m\002"
BLACK="$ESC[${DULL};${FG_BLACK}m\002"
RED="$ESC[${DULL};${FG_RED}m\002"
GREEN="$ESC[${DULL};${FG_GREEN}m\002"
YELLOW="$ESC[${DULL};${FG_YELLOW}m\002"
BLUE="$ESC[${DULL};${FG_BLUE}m\002"
VIOLET="$ESC[${DULL};${FG_VIOLET}m\002"
CYAN="$ESC[${DULL};${FG_CYAN}m\002"
WHITE="$ESC[${DULL};${FG_WHITE}m\002"
BRIGHT_BLACK="$ESC[${BRIGHT};${FG_BLACK}m\002"
BRIGHT_RED="$ESC[${BRIGHT};${FG_RED}m\002"
BRIGHT_GREEN="$ESC[${BRIGHT};${FG_GREEN}m\002"
BRIGHT_YELLOW="$ESC[${BRIGHT};${FG_YELLOW}m\002"
BRIGHT_BLUE="$ESC[${BRIGHT};${FG_BLUE}m\002"
BRIGHT_VIOLET="$ESC[${BRIGHT};${FG_VIOLET}m\002"
BRIGHT_CYAN="$ESC[${BRIGHT};${FG_CYAN}m\002"
BRIGHT_WHITE="$ESC[${BRIGHT};${FG_WHITE}m\002"
REV_CYAN="$ESC[${DULL};${BG_WHITE};${BG_CYAN}m\002"
REV_RED="$ESC[${DULL};${FG_YELLOW}; ${BG_RED}m\002"

run $*
