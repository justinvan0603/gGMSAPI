<?php

/* 
 * Plugin Name: Polylang Option-Tree Bridge
 * Description: Allows you to have separate Theme Options settings for each language. To switch between theme options language use language switcher in admin bar. <strong>Requires Option-Tree and Polylang plugins to work</strong>.
 * Version: 1.1
 * Author: Mateusz Palaczyk
 * License: MIT
 * Tags: polylang, option-tree, option tree, optiontree, multilingual option tree, option, tree, languages, language, theme, options, theme options, bridge
 */

class Polylang_Option_Tree { 
	public function plugins_loaded() {
		if( function_exists( 'pll_default_language' ) ) {
			add_filter( 'ot_options_id', array( $this, 'ot_option_id' ) );
		}
	}
	
	public function ot_option_id( $default ) {
		if( is_admin() ) {
			$user_lang = get_user_meta(get_current_user_id(), 'pll_filter_content', true);
			$selected_lang = filter_input( INPUT_GET, 'lang', FILTER_SANITIZE_STRING );
			$lang = $selected_lang !== null ? $selected_lang : $user_lang;
		} else {
			$lang = pll_current_language();
		}

		if( $lang AND $lang != 'all' AND $lang != pll_default_language() ) {
			return $default . '_' . $lang;
		}
		
		return $default;
	}
}

$pot = new Polylang_Option_Tree();
add_action( 'plugins_loaded',  array( $pot, 'plugins_loaded' ) );