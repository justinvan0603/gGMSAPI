=== Plugin Name ===
Contributors: emati
Tags: polylang, option-tree, option tree, optiontree, multilingual option tree, option, tree, languages, language, theme, options, theme options, bridge
Requires at least: 3.5
Tested up to: 4.6.1
Stable tag: trunk
License: MIT
License URI: http://opensource.org/licenses/MIT

This plugin is a bridge between polylang and option tree plugin which enables you to have different theme options for different language.

== Description ==

This plugin is a bridge between polylang and option tree plugin which enables you to have different theme options for different language.

**It's only works for themes that use Option Tree plugin for theme options.**

= Dependencies: =
**At least:**

* Polylang v1.5
* Option-Tree v2.3.0

**Tested up to:**

* Polylang v2.0.4
* Option-Tree v2.6.0

= How it works =

After setting up your languages and option tree settings, go to `Theme Options` and use language switcher on the admin bar to switch options for different language.

== Installation ==

1. Install and activate polylang plugin (at least 1.5)
2. Install and activate option-tree plugin (at least 2.3.0)
3. Upload `multilingual-option-tree` folder to the `/wp-content/plugins/`
4. Activate the plugin through the 'Plugins' menu in WordPress
5. Setup your polylang languages
6. Setup you theme options
7. Everything should be working now. Go to `Theme Options` and use language switcher on the admin bar to switch options for different language.

== Screenshots ==

1. You can set theme options for different languages while your in "Theme Options" settings page and changing language in the language switcher.

== Changelog ==

= 1.1 =
* Major fix: fixed bootstraping to run on plugins_loaded action to ensure that polylang and option-tree are loaded.

= 1.0 =
* Initial plugin submission