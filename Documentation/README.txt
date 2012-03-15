Rawbots README

Rawbots is a game inspired from Nether Earth. It is written in C#, and is meant to be cross-platform
on Windows, Mac OS X and Linux using Mono and MonoDevelop.

* Dependencies

Rawbots comes with libraries such as OpenTK, Tao and QuickFont inside the Libraries folder.
OpenTK and Tao provide C# bindings to OpenGL and GLUT, while QuickFont is an OpenTK extension
that provides support for font drawing.

If you wish to download those libraries separately, here is where they can be obtained:

OpenTK: http://www.opentk.com
QuickFont: http://www.opentk.com/project/QuickFont
Tao Framework: http://sourceforge.net/projects/taoframework/

Besides the libraries used by Rawbots, you will need the proper runtime environment and
development tools. On Windows, you can use the Microsoft .NET Framework and Visual Studio.

The usage of the .NET Framework and Visual Studio is optional, since Rawbots is fully compatible
with Mono and MonoDevelop. On Linux and Mac OS X this is the only option available for obvious reasons.

Here is the download page for Mono:
http://www.go-mono.com/mono-downloads/download.html

If you are on Windows, you will need to install "Gtk# for .NET" and "Mono for Windows, Gtk# and XSP" as well.

Once Mono is installed, you can download and install MonoDevelop:
http://monodevelop.com/

Installation should be pretty straightforward. If you're on Linux, there's nothing to say really,
just get the Mono runtime and MonoDevelop from your distribution's package manager.

* Licensing

Rawbots is licensed under the Mozilla Public License (MPL) 2.0. A copy of the license can be found in the
LICENSE file, otherwise the license can be found at http://www.mozilla.org/MPL/2.0/index.txt

MPL 2.0 was chosen because it is a weak copyleft license that would allow others to build on our work
past the end of this course. When spending a lot of time on a team project it's usually better if the
end result is not wasted and forgotten afterwards. We encourage future teams to use our work for reference
if they wish, or even build on top of it if the course allows for it.

* Version Control

The Rawbots source code is available on GitHub: https://github.com/rawbots/rawbots

You can obtain a copy of the source code through the GitHub web interface or using a git client.

If you are not familiar with git, it is a very powerful version control system. Unfortunately,
powerful tools made by power users are not necessarily easy to learn at first. However, by the end
of this project, all of our team members were comfortable enough with it.

Here is some help on git usage:

http://help.github.com/
https://github.com/FreeRDP/FreeRDP/wiki/Git-Version-Control

The read-only git URL for Rawbots is git://github.com/rawbots/rawbots.git

You can clone the repository without developer access using:
git clone git://github.com/rawbots/rawbots.git

* Compilation

If you have properly set up your development environment and the dependencies as described earlier,
Rawbots compilation should be straightforward. Open the solution file (Rawbots.sln) either in Visual Studio
or MonoDevelop, build and run. That's it, nothing more complicated than that.

