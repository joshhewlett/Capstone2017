#! /bin/sh

# Example install script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build

# This link changes from time to time. I haven't found a reliable hosted installer package for doing regular
# installs like this. You will probably need to grab a current link from: http://unity3d.com/get-unity/download/archive
echo 'Downloading from https://unity3d.com/get-unity/download?thank-you=update&download_nid=48329&os=Mac: '
curl -o Unity.pkg https://unity3d.com/get-unity/download?thank-you=update&download_nid=48329&os=Mac

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /
