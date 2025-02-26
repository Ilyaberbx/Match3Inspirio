
# Match3 Inspirio

This project provides simple match3 game integrated with WebView

## Installation

No additional installs needed after project cloned# Documentation

## App Initialization
The project uses `AppInitialization.cs` for initialization. This configuration comes from the client:
```
Assets/Resources/Configs/AppInitializationConfiguration.asset
```
It can be replaced by a server-side implementation if needed.

## Gameplay Configuration
All gameplay features are configurable via client-side Scriptable Objects:
```
Assets/Resources/Configs/Gameplay
```

## Analytics & Push Notifications
### Appsflyer
Configuration file:  
```
Assets/Resources/Configs/Appsflyer/AppsflyerServiceSettings.asset
```

### OneSignal
Configuration file:  
```
Assets/Resources/Configs/OneSignal/OneSignalServiceSettings.asset
```