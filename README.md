# This a tutorial for "BabyNI", a mockup of the well established SaaS "Network Insight" by Yuvo.

## Contents
The project contains 2 iterations, demonstrating the progress of my knowledge from the start of the internship until the day of submission.
The first iteration is a console app (class library projetct) found under the "main" branch, the second iteration is a microservices (ASP.NET Core Web API) implementation found under the "staging" branch.

## Requirements
To replicate the first iteration (console app), install node 18 and .Net 7.0, and start by cloning the code.
To replicate the second iteration, install all the above, and if you need to run all the docker containers, docker is required.

## Instructions
1-  To run the first iteration, clone the "main" branch, then open BabyNI.sln in Visual Studio, which is a console app containing the watcher, parser, loader and aggregator.
Right click on the solution in file explorer and click build solution, and then run the BabyNI.exe build file found in BabyNI/bin/debug/win64.
The API Controller can be started by running the BabyAPI.exe build file found in BabyAPI/bin/debug/win64.
The React and NextJs app can be started by opening the UI folder in Visual Studio Code, then running npm install in the console (ctrl + `), and then by running npm run dev and visiting http://localhost:3000 in the browser.

2- To run the second iteration, clone the "staging" branch, then open BabyNI.sln in Visual Studio, and build the projects (ctrl + shift+ b), then execute them either one by one (parser then loader then aggregator then BabyAPI) or modify the properties of the solution to start all the projects at the same time. Also, you can run the executable of each API from their respective /bin/debug/win64 directories. Note that for these APIs to work in windows, you need to comment out each <RuntimeIdentifier>linux-x64</RuntimeIdentifier> line the .csproj file for each API. Also, change the rootDirectory in the parser/watcher/FileWatcher.cs from rootDirectory = @"C:\Watcher" to the desired watcher directory, or simply create a "Watcher" directory under the "C" drive.

3- To run the second iteration, with support for docker and containerization, clone the "staging" branch, and start your docker engine. Then continue with the instructions found in the readme.md file fount under the "staging" branch.
Note that the <RuntimeIdentifier>linux-x64</RuntimeIdentifier> line in .csproj should be uncommented (if previously commented out in step 2) before running the containers, because docker supports linux images only.
Also, change the rootDirectory in the parser/watcher/FileWatcher.cs file from rootDirectory = @"C:\Watcher" to rootDirectory = @"/app/FileDropZone".

#### **Note that when launching the API Controller using the Debug mode in Visual Studio, please select the http profile, because the https profile uses a different port that doesn't work with the incoming fetch requests from the React App. Another option is to change the port in each url in the .env.local and .env.production files to match with the ports found in the https profile, and also change http:// to https:// in each url.
