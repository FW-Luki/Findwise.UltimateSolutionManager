# Findwise.UltimateSolutionManager
The Ultimate Windows Solution Manager

## Overview
The Ultimate Solution Manager is a Windows desktop application which helps organise any installation process on any Windows machine.
It allows to setup installation steps and save them for later use on either the same or another machine.

## Core
The core of the application is a Windows Forms application which allows to add modules to the list in a desired order.

Every module has its configuration. The application allows to setup modules' configurations and save it for later use.

If a configuration contains a bindable property the application allows to bind that property to the master configuration property which allows to change all bound properties with one click.

Main window of the application allows to:
- view all selected modules
- check their status install modules separately or with single click in order set by the list
- uninstall modules separately

## Modules
The application loads any usable modules from Modules folder placed in the same directory as the application main executable file.

Currently, most of the modules are designed to setup Sharepoint Search solution.
There are also modules for creating table in database and for manage external PowerShell scripts.

## Project structure
The projecct consists of main executable project dependant on several core libraries. All these projects are located in solution main directory.

Module projects are dependant on core libraries. They are located in Modules directory.

There is one library, Findwise.Configuration, on which most of the project depend. It is added as submodule to the repository.

## Contribution
The project is open for any improvements. For further information refer to the contributing.md file.

## Further development
The application is in an early stage of development and needs further improvements as well as adding new features. For details refer to the todo.md file.
