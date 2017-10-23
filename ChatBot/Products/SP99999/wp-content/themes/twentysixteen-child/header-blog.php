<?php
/**
 * The template for displaying the header
 *
 * Displays all of the head element and everything up until the "site-content" div.
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

?><!DOCTYPE html>
<!-- Get Option HomePage -->
<?php




$logo = ot_get_option( 'logo',array() );

if($logo['background-image']=='')
{
 $logo_url = '';
}
else
{
  $logo_url = $logo['background-image'];
}
$logo_background = ot_get_option( 'logo_background',array() );
$menu_background = ot_get_option( 'menu_background',array() );
$favicon = ot_get_option( 'favicon',array() );

?>
<!-- Get Option HomePage -->
<html <?php language_attributes(); ?> class="no-js">
<head>
	<meta charset="<?php bloginfo( 'charset' ); ?>">
	<meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
	
	<?php wp_head(); ?>
    <link rel="icon" href="<?=$favicon["background-image"]?>" type="image/x-icon">
    <link href="https://fonts.googleapis.com/css?family=Raleway:500,500i,700" rel="stylesheet">
	<link href="https://fonts.googleapis.com/css?family=Open+Sans:400,400i,700&amp;subset=vietnamese" rel="stylesheet">
	<link rel="stylesheet" href="/wp-content/plugins/js_composer/assets/css/js_composer.min.css">
    <link rel="stylesheet" href="<?php bloginfo( 'stylesheet_directory' ); ?>/html/css/style.css">
	<link rel="stylesheet" href="<?php bloginfo( 'stylesheet_directory' ); ?>/html/css/animate.css">
	
    <!--[if lt IE 10]>
    <div style="background: #212121; padding: 10px 0; box-shadow: 3px 3px 5px 0 rgba(0,0,0,.3); clear: both; text-align:center; position: relative; z-index:1;"><a href="http://windows.microsoft.com/en-US/internet-explorer/"><img src="images/ie8-panel/warning_bar_0000_us.jpg" border="0" height="42" width="820" alt="You are using an outdated browser. For a faster, safer browsing experience, upgrade for free today."></a></div>
    <script src="<?php bloginfo( 'template_url' ); ?>/html/js/html5shiv.min.js"></script>
		<![endif]-->
</head>

<body>
<div id="page" class="page site page-blog">
	
		<!-- Page Header-->
      <header class="page-head header-corporate text-center" style="padding-top:2vw;padding-bottom:3vw;">
        <!-- RD Navbar-->
        <a href="/" class="brand-name">
                    <img src="<?=$logo_url?>" /></a>
      </header>
      <!-- Page Content-->

		<div id="content-template" class="site-content-template">
