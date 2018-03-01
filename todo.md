# ToDo
A list of improvements and new features that need to be done in order to make the Ultimate Solution Manager a complete product.

## Core
- Add missing keyboard shortcuts/hotkeys (if Refactor step is taken, it is most likely that this step will no longer be applicable)
- Refactor:
  - Separate logic to Core assembly 
    - Make it generic enough to work with any frond-end technology (WinForms, WPF, Web and possibly UWP)
    - Separate interfaces with fully bindable properties
  - Add front-end applications
  
## Modules
### PowershellScriptExecutor
- Add support for Install/Uninstall/CheckStatus methods, if needed

### SharepointSolutionPackageInstaller
- Add building Visual Studio solution feature on project save
  - Implement template mechanism
  - Implement multiple builds based on number of Master Configurations
  - Implement Wsp package storage
- Separate BcsConnectorInstaller to its own assembly since it has to depend on WspInstaller and ContentSourceCreator

### Add SharePoint Framework (SPFx) Package Installer
- Add and implement new module
