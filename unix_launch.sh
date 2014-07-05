#!/bin/bash

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

path=$([[ $1 = /* ]] && echo "$1" || echo "$PWD/${1#./}")

echo $path

read -p "Enter your Huckleberry username:" un
read -sp "Enter your Huckleberry password:" pn

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

lib_list=$(curl -s "https://huckleberry.runsafe.no/lib_list.php");
lib_dir="${path}libs/"

if [ ! -d "$lib_dir" ]; then
  mkdir -p "$lib_dir"
  echo "lib directory not found, creating new one"
fi

IFS=',' read -a libs <<< "$lib_list"

for lib in "${libs[@]}"
do
	lib_name=$(echo $lib | awk -F":" '{print $1}')
	lib_hash=$(echo $lib | awk -F":" '{print $2}')
	lib_path="$lib_dir$lib_name"
    echo "Checking library: $lib_name"
	
	lib_bin=("${lib_bin[@]}" "${path}libs/$lib_name")
			
	if [ ! -f ./libs/$lib_name ]; then
		echo "Library does not exist, downloading"
		curl -s -o "$lib_path" "https://huckleberry.runsafe.no/client/libs/$lib_name"
	else
		echo "Library exists, checking local hash with server version"
		local_hash=$(__rvm_md5_for "$lib_path");
		if [ "$local_hash" == "$lib_hash" ]; then
			echo "Local version is up-to-date."
		else
			echo "Hash mis-match, downloading library..."
			curl -s -o "$lib_path" "https://huckleberry.runsafe.no/client/libs/$lib_name"
		fi
	fi
done

lib_list=$(curl -s "https://huckleberry.runsafe.no/assets.dat");

delim=$(awk 'BEGIN{printf "%c", 31}')
delim_r=$(awk 'BEGIN{printf "%c", 30}')
IFS="$delim" read -a assets <<< "$lib_list"
IFS="$delim_r" read -a asset_dirs <<< "${assets[0]}"

for asset_dir in "${asset_dirs[@]}"
do
	asset_dir="${path}assets/$asset_dir"
	if [ ! -d "$asset_dir" ]; then
	  mkdir -p "$asset_dir"
	  echo "Creating asset directory: $asset_dir"
	fi
done

IFS="$delim_r" read -a assets <<< "${assets[1]}"
for asset in "${assets[@]}"
do	
	asset_name=$(echo $asset | awk -F":" '{print $1}')
	asset_hash=$(echo $asset | awk -F":" '{print $2}')
	
	asset_path="${path}assets/$asset_name"
	
	if [ ! -f $asset_path ]; then
		echo "Downloading asset: $asset_name"
		curl -s -o "$asset_path" "https://huckleberry.runsafe.no/client/assets/$asset_name"
	else
		local_hash=$(__rvm_md5_for "$asset_path");
		if [ "$local_hash" == "$asset_hash" ]; then
			echo "Asset up-to-date: $asset_name"
		else
			echo "Downloading asset: $asset_name"
			curl -s -o "$asset_path" "https://huckleberry.runsafe.no/client/assets/$asset_name"
		fi
	fi
done

libbys=$(IFS=':'; echo "${lib_bin[*]}")
java -Djava.library.path="libs" -cp "${libbys}" net.minecraft.client.main.Main --username $user --session $key --version 1.6.4
