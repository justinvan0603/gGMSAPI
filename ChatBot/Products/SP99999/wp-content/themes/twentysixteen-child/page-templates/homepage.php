<?php
/**
 * Template Name: Home Page
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

get_header(); ?>


 <!-- Page Content-->
 <?php $main_slider = ot_get_option('main_slider',array()); 
	
 ?>
 <!-- main slideshow -->
 <marquee width="100%" height="30" style="line-height:30px;color:#333;font-size:12px;display:block;background:url(<?php echo home_url()?>/wp-content/uploads/2017/05/i_01.gif) center center no-repeat;background-size:cover;">Bộ Công An - Công An Thành Phố Hồ Chí Minh - Phòng Viễn Thông Tin Học Bộ Công An - Công An Thành Phố Hồ Chí Minh - Phòng Viễn Thông Tin Học Bộ Công An - Công An Thành Phố Hồ Chí Minh - Phòng Viễn Thông Tin Học Bộ Công An - Công An Thành Phố Hồ Chí Minh - Phòng Viễn Thông Tin Học Bộ Công An - Công An Thành Phố Hồ Chí Minh - Phòng Viễn Thông Tin Học</marquee>
 <div class="top-content offset-top-15 offset-bottom-15">
	<div class="col-md-7 slider" style="padding-left:0;padding-right: 0;
    box-shadow: 0px 0px 10px black;">
		   <div data-items="1" data-stage-padding="0" data-loop="false" data-animateOut="fadeOut" data-animateIn="fadeIn" data-margin="0" data-dots="true" data-mouse-drag="true" data-nav="true" data-autoplay="true" id="home-slider" class="<?=count($main_slider)>1?'owl-carousel owl-carousel-1 main-slider home-slider':'main-slider home-slider'?>">
          	<?php
          		
				
				$button_tu_van_mien_phi = ot_get_option('button_tu_van_mien_phi','#');
				//$text_tu_van_mien_phi = __('Liên hệ với chúng tôi');
          		foreach($main_slider as $slide)
              {
                ?>
          			<section style="background-image:url(<?=$slide['image']?>); background-position:center center;background-size:cover;height:310px;" class="bg-image bg-image-1 section-30 text-center inset-left-10 inset-right-10">
					<a href="<?=$slide['link']?>">
					
					<div style="position:absolute;text-align:justify;padding:0 40px 0 30px;">
                <h5 style="font-size:18px;" class="txt-white wpb_animate_when_almost_visible fadeInDownBig"><?=$slide['title']?></h5>
				<br />
				<h6 style="font-size:16px;" class="txt-white wpb_animate_when_almost_visible fadeInUpBig"><?=$slide['description']?></h6>
				</div>
				</a>
                </section>
          			<?php
              }
          	?>
        </div>
	</div>
	<div class="col-md-5 tin-noi-bat" style="padding-right:0;">
		<div class="content-tin">
			<h2 class="title-navi">Tin nổi bật</h2>
			<div class="tin-items-div">
				<?php
			$tin = get_posts(array('post_type'      => 'post',
    'posts_per_page' => 5,
    'post_status'    => 'publish','order'          => 'DESC',
    'orderby'        => 'date','category__not_in'=>array('23')));
			foreach($tin as $t)
			{
				echo "<div class='tin-items'><a href='".get_permalink($t)."' title='".$t->post_title."'><i class='fa fa-newspaper-o' aria-hidden='true'></i>".$t->post_title."</a></div>";
			}
		?>
			</div>
		</div>
	</div>
	<div class="cleared"></div>
 </div>
     <div class="banner-center offset-bottom-15">
		<div class="qc-items">
		<?php
			$banner_quang_cao = ot_get_option('banner_quang_cao');
			foreach($banner_quang_cao as $banner)
			{
				echo "<div class='qc-item col-md-3'><a href='".$banner['link']."'><img src='".$banner['image']['background-image']."' alt='".$banner['title']."' /></a></div>";
			}
		?>
		</div>
	 </div>
	 <div class="cleared"></div>
        <!-- end Main slideshow-->
      <main class="page-content offset-bottom-15 offset-top-15">
		<div class="left-content-div col-md-7" style="padding-left:0;">
			
			<?php
				$left_content = ot_get_option('left_content');
				$j = 1;
				foreach($left_content as $left)
				{
					echo "<div class='left-content left-content-".$j." offset-bottom-15' id='left-content-".$i."'>";
					echo "<h2 class='title-navi'>".$left['title']."</h2>";
					echo "<div class='left-content-items offset-top-10'>";
					$cat_id = $left['category'];
					$tin = get_posts(array('post_type'      => 'post',
						'posts_per_page' => 5,
						'post_status'    => 'publish','order'          => 'DESC',
						'orderby'        => 'date','category'=>$cat_id));
					$i = 1;
					foreach($tin as $t)
					{
						if($i==1)
						{
							echo "<a href='".get_permalink($t)."' style='float:left;'><image src='".get_the_post_thumbnail_url($t,'thumbnail')."' /></a>";
							echo "<div class='left-content-item left-content-item-image offset-bottom-10'>";
							echo "<h3 class='offset-bottom-10'><a title='".$t->post_title."' href='".get_permalink($t)."'>".wp_trim_words($t->post_title,20,'...')."</a></h3>";
							echo "<p>".wp_trim_words(wp_strip_all_tags($t->post_content),90,'...')."</p>";
							echo "</div>";
							echo "<div class='cleared'></div>";
						}
						else
						{
							echo "<div class='left-content-item offset-bottom-10'>";
							echo "<h4><a title='".$t->post_title."' href='".get_permalink($t)."'><i class='fa fa-newspaper-o' aria-hidden='true'></i>".wp_trim_words($t->post_title,20,'...')."</a></h4>";
							echo "</div>";
						}
						$i++;
					}
					echo "</div>";
					echo "</div>";
					$j++;
				}
			?>
		</div>
		<?php get_sidebar('custom'); ?>
      </main>
      <!-- Page Content-->
<div class="cleared"></div>
<?php

get_footer();
