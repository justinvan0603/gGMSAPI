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
    
	<link rel="stylesheet" href="
<?php echo plugins_url(); ?>/js_composer/assets/css/js_composer.min.css">
    <link rel="stylesheet" href="<?php bloginfo( 'stylesheet_directory' ); ?>/html/css/style.css">
	<link rel="stylesheet" href="<?php bloginfo( 'stylesheet_directory' ); ?>/html/css/animate.css">
	<link rel="stylesheet" href="<?php bloginfo( 'stylesheet_directory' ); ?>/html/css/jquery-ui.css" />
<link rel="stylesheet" href="<?php bloginfo( 'stylesheet_directory' ); ?>/html/css/jquery.fancybox.css" />
    <!--[if lt IE 10]>
    <div style="background: #212121; padding: 10px 0; box-shadow: 3px 3px 5px 0 rgba(0,0,0,.3); clear: both; text-align:center; position: relative; z-index:1;"><a href="http://windows.microsoft.com/en-US/internet-explorer/"><img src="images/ie8-panel/warning_bar_0000_us.jpg" border="0" height="42" width="820" alt="You are using an outdated browser. For a faster, safer browsing experience, upgrade for free today."></a></div>
    <script src="<?php bloginfo( 'template_url' ); ?>/html/js/html5shiv.min.js"></script>
		<![endif]-->
</head>

<body>
<div id="page" class="page site">
	
		
		<!-- Page Header-->
      <header class="page-head header-corporate">
		<div class="shell">
        <!-- RD Navbar-->
        <div class="rd-navbar-wrap">
          <nav class="rd-navbar" data-layout="rd-navbar-fixed" data-sm-layout="rd-navbar-fullwidth" data-md-layout="rd-navbar-fullwidth" data-lg-layout="rd-navbar-static" data-md-device-layout="rd-navbar-fixed" data-lg-device-layout="rd-navbar-static" data-sm-stick-up-offset="50px" data-md-stick-up-offset="150px" data-lg-stick-up-offset="150px">

            <div class="rd-navbar-inner">
              <!-- RD Navbar Top Panel-->
              <div class="rd-navbar-wrap">
                
                <!-- RD Navbar Panel-->
                <div class="rd-navbar-panel" style="background-image:url(<?=$logo_background['background-image']?>);background-color:<?=$logo_background['background-color']?>;background-size:<?=$logo_background['background-size']?>;">
                  <!-- RD Navbar Toggle-->
                  <button data-rd-navbar-toggle=".rd-navbar-nav-wrap" class="rd-navbar-toggle"><span></span></button>
                  <!-- RD Navbar Brand-->
                  <div class="rd-navbar-brand"><a href="/" class="brand-name">
                    <img class='no-popup' style="width:100%;" src="<?=$logo_url?>" /></a>
					<?php
						$logo_name = ot_get_option('logo_name');
						if($logo_name!='')
						{
							echo '<span class="logo_name">'.$logo_name.'</span>';
						}
					?>
					</div>
					
					
                </div>
                <div class="rd-navbar-nav-wrap" style="background-image:url(<?=$menu_background['background-image']?>);background-color:<?=$menu_background['background-color']?>">
                  <!-- RD Navbar Nav-->
                  <?php
					//$lang = 'vi';
                  	//$menu_items = array();
					//if(function_exists(pll_current_language)) {
					//	$lang = pll_current_language();
					//}
					//if($lang=='vi') $menu_items = wp_get_nav_menu_items('Main Menu');
					//else $menu_items = wp_get_nav_menu_items('Main Menu En');
					$menu_items = wp_get_nav_menu_items('Main Menu');
                  	$menu_list='';
                  	$menu_list .= '<ul class="rd-navbar-nav">' ."\n";
					$object_id = get_queried_object_id();
					
					$i= 1;
                  	foreach( $menu_items as $menu_item ) {
                        if( $menu_item->menu_item_parent == 0 ) {
							$active = false;
                            $parent = $menu_item->ID;
				$bool = false;
                            $menu_array = array();
                            foreach( $menu_items as $submenu ) {
                                if( $submenu->menu_item_parent == $parent ) {
                                    $bool = true;
									$subactive = false;
									if($object_id==$submenu->object_id) {
										$active = true;
										$subactive = true;
									}
                                    $menu_array[] = '<li><a href="' . $submenu->url . '" object_id="'.$submenu->object_id.'">' . $submenu->title . '</a></li>' ."\n";
                                }
                            }
                            if( $bool == true && count( $menu_array ) > 0 ) {

                                if($object_id==$menu_item->object_id) $active = true;
								
								$menu_list .= '<li id="menu-item-'.$i.'">' ."\n";
                                $menu_list .= '<a href="'. $menu_item->url.'">' . $menu_item->title . '</a>' ."\n";

                                $menu_list .= '<ul class="rd-navbar-dropdown">' ."\n";
                                $menu_list .= implode( "\n", $menu_array );
                                $menu_list .= '</ul>' ."\n";

                            } else {

                                if($object_id==$menu_item->object_id) $active = true;
								$menu_list .= '<li id="menu-item-'.$i.'">' ."\n";
                                $menu_list .= '<a href="' . $menu_item->url . '">' . $menu_item->title . '</a>' ."\n";
                            }
							$i++;
                        }

                        // end <li>
                        $menu_list .= '</li>' ."\n";
                    }
                  	
                  	echo $menu_list;
                  ?>
                 
                    
                    
                  </ul>
                  <!--RD Search-->
				  
                  <style>
				  .rd-navbar-static .rd-navbar-nav > li > a,.rd-navbar-static .rd-navbar-nav > li > a > i,.rd-navbar-static .rd-navbar-nav > li > a > i:before{
				  <?php
					$menu_font = ot_get_option("menu_font");
					if(!empty($menu_font)){
						if($menu_font['font-color']!="") echo "color:".$menu_font['font-color'].";";
						if($menu_font['font-family']!="") echo "font-family:".$menu_font['font-family'].";";
						if($menu_font['font-size']!="") echo "font-size:".$menu_font['font-size'].";";
						if($menu_font['font-weight']!="") echo "font-weight:".$menu_font['font-weight'].";";
					}
				  ?>
				  }
				  
				  <?php
					$sub_menu_style= ot_get_option("sub_menu_style");
					if($sub_menu_style=="2")
					{
						?>
						.rd-navbar-static .rd-navbar-nav-wrap{
							position:relative;
						}
						
						.rd-navbar-static .rd-navbar-nav > .rd-navbar-submenu.rd-navbar--has-dropdown{
							position:initial;	
						}
						.rd-navbar-static .rd-navbar-nav > .rd-navbar-submenu > .rd-navbar-dropdown{
								    left: 0 !important;
								width: 100%;
								transform: none !important;
								top: 40px;
								text-align: center;
								border-radius:0;
								    box-shadow: 0 2px 5px 0 rgba(0,0,0,.75);
						}
						.rd-navbar-static .rd-navbar-nav > .rd-navbar-submenu > .rd-navbar-dropdown > li{
						
								display:inline-block;
								line-height:30px;
						}
						
						<?php
					}
					else
					{
						echo '.stick-menu{display:none !important;}';
					}
					?>
					.rd-navbar-static .rd-navbar-nav > .rd-navbar-submenu > .rd-navbar-dropdown{
					<?php
					$sub_menu_background = ot_get_option("sub_menu_background");
					if(!empty($sub_menu_background))
					{
						echo "background:".$sub_menu_background['background-color'].";";
					}
				  ?>
					}
					.rd-navbar-static .rd-navbar-nav > .rd-navbar-submenu > .rd-navbar-dropdown > li > a
					{
						<?php
					$sub_menu_font = ot_get_option("sub_menu_font");
					if(!empty($sub_menu_font))
					{
						echo "color:".$sub_menu_font['font-color'].";";
						echo "font-weight:".$sub_menu_font['font-weight'].";";
						echo "font-size:".$sub_menu_font['font-size'].";";
					}
				  ?>
					}
				  </style>
				  <ul class="stick-menu"></ul>
                </div>
              </div>
            </div>
          </nav>
        </div>
		</div>
      </header>
      <!-- Page Content-->

		<div id="content-template" class="site-content-template">
			<div class="shell">