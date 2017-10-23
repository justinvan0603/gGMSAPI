<?php
/**
 * The template for displaying the footer
 *
 * Contains the closing of the #content div and all content after
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */
?>
			</div>
		</div><!-- .site-content -->

<!-- Page Footer-->
<?php
	$lang = 'vi';
	if(function_exists(pll_current_language)) $lang = pll_current_language();
    $footer_background = ot_get_option( 'footer_background',array() );
    $bottom_background = ot_get_option( 'bottom_background',array() );
	$footer_about= ot_get_option( 'footer_about','' );
$phone_number = ot_get_option( 'footer_phone','' );
$cell_phone_number = ot_get_option( 'footer_cell_phone','' );
$address = ot_get_option( 'footer_address','' );
$email = ot_get_option( 'footer_email','' );
$menu_footer = ot_get_option( 'menu_footer',array() );
$custom_menu_1 = ot_get_option( 'custom_menu_1',array() );
//print_r($menu_footer);
  
?>
      
      <footer class="bottom-footer" style="background: <?=$bottom_background['background-color']?>;">
        <div class="shell">
            <div class="cell-xs-12 text-center text-md-left section-15">
              <p class="privacy text-center txt-white" style="font-size: 15px;">Bản quyền thuộc về Công an TP.Hồ Chí Minh <span id="copyright-year"></span> - <a style="color: #cdcdcd;" href="http://gsoft.com.vn"><?php echo __('Thiết kế website');?></a> <?php echo __('bởi GSOFT');?></p>
			  <div class="clearfix"></div>
            </div>
        </div>
      </footer>		
<!-- Page Footer-->
	
</div><!-- .site -->
<?php wp_footer(); ?>


 <script src="<?php bloginfo( 'stylesheet_directory' ); ?>/html/js/core.min.js"></script>
 <script src="<?php bloginfo( 'stylesheet_directory' ); ?>/html/js/jquery.fancybox.js"></script>
    <script src="<?php bloginfo( 'stylesheet_directory' ); ?>/html/js/script.js"></script>
	<script src="<?php echo plugins_url()?>/js_composer/assets/lib/waypoints/waypoints.min.js"></script>
	<script>
		
		$(function(){
			jQuery('.single-page img').on('click',function(){
				$.fancybox.open(
					{
						src  : $(this).attr('src'),
					},
				);
			});
			function show_map($select)
			{
				$select.parent().parent().find('.map').html($select.val());
			}
			var $select = $('#lien-he-select');
			$select.parent().parent().append('<div class="map"></div>');
			$cur_url = window.location.href;
			show_map($select);
			$select.on('change',function(){
				show_map($select);
			});
			$('.rd-navbar-nav li a').each(function(index){
				value = $(this);
				$url = value.attr('href');
				if($cur_url==$url)
				{
					value.addClass('active');
					value.parent().addClass('active');
					
					if(value.parent().parent().parent().attr('class').indexOf('rd-navbar-submenu')>=0)
					{
						var parent = value.parent().parent().parent();
						parent.addClass('active');
					}
				}
			});
			$('.rd-navbar-dropdown li a').on('click',function(e){
				var $liparent = $(this).parent();
				var $ulparent = $liparent.parent();
				$ulparent.find('.active').removeClass('active');
				$liparent.addClass('active');
				var target = $(this).attr('href');
				if( target.length && target.indexOf('#')>=0) {
					//e.preventDefault();
					var movetarget = $('#'+target.split('#')[1]+'');
					$('html, body').stop().animate({
						scrollTop: movetarget.offset().top - 100
					}, 3000);
				}
			});
			
			//$('.rd-navbar-submenu-toggle').hide();
			$(window).scroll(function(){
				if($(window).scrollTop() > 120){
					$('.rd-navbar-static .rd-navbar-nav-wrap').addClass('nav-bar-fixed');
					if($('#logo_menu').length)
					{
					}
					else
					{
						
					}
				}
				else
				{
					$('.rd-navbar-static .rd-navbar-nav-wrap').removeClass('nav-bar-fixed');
					$('#logo_menu').remove();
				}
				var lydo = $('.ly-do-tai-sao');
				jQuery(".online-reputation").waypoint(function() {
					jQuery(".wpb_animate_when_almost_visible:not(.wpb_start_animation)").addClass("wpb_start_animation animated");
				});
			});
			
			
			
			$('.video-item h4').on('click',function(){
				$('.video-item').removeClass('open');
				$("video").each(function () { this.pause() });
				$(this).parent().addClass('open');
			});
		});
		
	</script>
	<script> function printThis() { window.print(); } </script>
	<script src="<?php bloginfo( 'stylesheet_directory' ); ?>/html/js/jquery-ui.js"></script>
	
</body>
</html>