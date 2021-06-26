# UnityAdmobManager
A package for easily implementing GoogleMobileAds into a Unity Project. The GameObject linked to this script will also perpetuate through all your scenes (via `DontDestroyOnLoad`), making it easily accessible from any other MonoBehaviour. This package currently only loads `bannerView` and `interstitialAd` Ads. Banner ads can be set up to load by default on any scenes that you specify via the Unity Editor. Sample Ads are also included for testing.

# Prerequisites

This package requires the **GoogleMobileAds** package which can be first installed from https://github.com/googleads/googleads-mobile-unity/releases/tag/v6.0.1. You'll need to enter your **AppID** there.

# Installing and Setting Up

1. Install the package or clone this repo directly into your `Assets` folder
2. Create an empty gameobject in the scene that loads first
3. Add the `AdManager` component to that game object
4. Enter your Android and iOS Ad Unit IDs that you would have acquired from [Admob](https://apps.admob.com/v2/home).

And that's it! You're all set! Read on below for more configuration options (particularly for setting up scenes that need to automatiicaly load a banner).

# Additional Configurations

The configuration are available via the `Inspector` once the game object has been created.

- `Use Sample Ads` - Use sample Ad Mob IDs for all ads. Great for testing
- `Print Debug Messages` - Prints debug messages to the console, set this to false to hide them
- `Banner Position` - The default `AdPosition` for all banner ads
- `Banner Scene Names` - Array of scene names that this package will load `bannerVIew` automatiically load on entry

# Load an Interstitial Ad

To load an interstitial ad, simply grab the `AdManger` instance and call the `ShowInterstitial()` function.

# 
