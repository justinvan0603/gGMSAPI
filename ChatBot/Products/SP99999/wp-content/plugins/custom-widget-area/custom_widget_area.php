<?php
/**
Plugin Name: Custom Widget Area
Plugin URI: http://wordpress.opensourcedevelopers.net/downloads/
Description: A simple plugin to create custom widget area.
Author: Shakti Kumar
Version: 1.1
Author URI: http://opensourcedevelopers.net/
 */
 
register_sidebar( array(
		'name' => __( 'Blog Sidebar', 'THINHNQ' ),
		'id' => 'blog-sidebar',
		'description' => __( 'Blog Sidebar', 'THINHNQ' ),
		'before_widget' => '<div id="%1$s" class="cell-sm-12 cell-md-12 cell-md-push-3 %2$s offset-top-20">',
		'after_widget' => '</div>',
		'before_title' => '<h6 class="widget-title">',
		'after_title' => '</h6><div class="divider-lg offset-top-10"></div>',
	) );
