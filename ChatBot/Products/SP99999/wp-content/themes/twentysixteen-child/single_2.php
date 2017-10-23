<?php
/**
 * The template for displaying all single posts and attachments
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */
$categories = get_field_object('chuyen_muc',$post->ID);
if(isset($categories)&&$categories['value']=='130')
	get_header(); 
else
	get_header('blog');
?>
<?php
	$breadcrumb_background = ot_get_option('breadcrumb_background',array());
	$b_image = $breadcrumb_background['background-image'];
	$b_color = $breadcrumb_background['background-color']; 
	if(have_posts())
	{
		$background_image = get_field_object('background_image',$post->ID);
		if(isset($background_image)&&$background_image['value']!='')
		{
			$b_image = $background_image['value'];
		}
		$background_color = get_field_object('background_color',$post->ID);
		if(isset($background_color)&&$background_color['value']!='')
		{
			$b_color = $background_color['value'];
		}
		//echo $post-> ID;
		
		
		
		
		//$args = array( 'post_type' => 'attachment', 'posts_per_page' => -1, 'post__in' => $images, 'orderby' => 'post__in' ); 
		//$images2 = get_posts( $args );
		//print_r($images2);
	}
	
					
?>
<!-- Page Content-->
<main class="page-content single-page">

    <?php
		$background_title = get_field_object('background_title',$post->ID);
		$background_description = get_field_object('background_description',$post->ID);
		$background_title_color = get_field_object('background_title_color',$post->ID);
		$background_description_color = get_field_object('background_description_color',$post->ID);
		$background_style = get_field_object('background_style',$post->ID);
		$hide_main_title = get_field_object('hide_main_title',$post->ID);
		
		if($b_image!=''||$b_color!='')
		{
			?>
			<section class="bg-breadcrumbs <?=$background_style['value']?>" style="background-image:url(<?=$b_image?>);background-color: <?=$b_color?>;background-size:cover;">
			<?php
				if(isset($background_title)&&$background_title['value']!='')
				{
					?><h1 style="text-transform:uppercase;color:<?=$background_title_color['value']?>" class="col-md-6"><?=$background_title['value']?></h1>
					<?php
				}
				if(isset($background_description)&&$background_description['value']!='')
				{
					?><div style="color:<?=$background_description_color['value']?>" class="col-md-6 text-justify"><?=$background_description['value']?></div>
					<?php
				}
			?>
			<?php
				
				if(isset($categories)&&$categories['value']=='130')//co so id = 130
				{
					$dia_chi = get_field_object('dia_chi',$post->ID);
					$so_dien_thoai = get_field_object('so_dien_thoai',$post->ID);
					$email = get_field_object('email',$post->ID);
					echo '<div style="color:'.$background_description_color['value'].'" class="col-md-12 text-center">';
					echo '<h3 style="text-transform:uppercase;color:'.$background_description_color['value'].'" class="col-md-12">'.get_the_title().'</h3>';
					echo '<p style="margin-top:10px;color:'.$background_description_color['value'].'">'.$post->post_content.'</p>';
					echo '<p><span style="color:'.$background_description_color['value'].'"  class="inline-block"><i class="fa fa-map-marker fa-2x" aria-hidden="true"></i><br />'.$dia_chi['value'].'</span><span style="color:'.$background_description_color['value'].'"  class="inline-block"><i class="fa fa-phone fa-2x" aria-hidden="true"></i><br />'.$so_dien_thoai['value'].'</span><span style="color:'.$background_description_color['value'].'"  class="inline-block"><i class="fa fa-envelope-o fa-2x" aria-hidden="true"></i><br />'.$email['value'].'</span></span></p>';
					echo '</div>';
				}
			?>
			 </section>
			<?php
		}
	 ?>
	 <section class="cont-breadcrumbs section-15">
		<div class="shell">
					<?php if (function_exists('breadcrumbs')) breadcrumbs(__("Trang chủ")); ?>
				  </div>
	 </section>
	 
	 <?php
		if(isset($categories)&&$categories['value']=='130')//co so id = 130
		{
			echo '<section class="noi_dung_coso">';
			$noi_dung_coso = get_field('noi_dung',$post->ID);
			$i=1;
			foreach($noi_dung_coso as $nd){
			if($i%2==1) echo '<div class="flex-div">';
			?>
			<div id="col-<?=$i?>" class="col-md-<?=$nd['column']?>" style="background-color:<?=$nd['background_color']?>;background-image:url(<?=$nd['background_image']?>)">
				<div class="col-content col-content-<?=$nd['style']?>"><?=$nd['text']?></div>
				<style>
					#col-<?=$i?> .col-content,
					#col-<?=$i?> .col-content p,
					#col-<?=$i?> .col-content li,
					#col-<?=$i?> .col-content a,
					#col-<?=$i?> .col-content span{color:<?=$nd['text_color']?>}
				</style>
			</div>
			<?php
			if($i%2==0) echo '</div>';
			$i++;
			}
			echo '</section>';
			?>
			<section class="thu_vien_hinh_anh offset-top-50">
				<h3 class="text-center text-uppercase">Các hình ảnh về trường</h3>
				<div class="description text-center"><?=get_field('mo_ta_thu_vien',$post->ID);?></div>
				
				<div data-items="1" data-xs-items="1" data-sm-items="2" data-md-items="3" data-thumbs="false"  data-stage-padding="0" data-loop="true" data-margin="0" data-dots="false" data-mouse-drag="true" data-nav="true" data-autoplay="true" class="owl-carousel owl-carousel-1 main-slider qdcslider">
									<?php
									$thu_vien_hinh_anh = acf_photo_gallery('thu_vien_hinh_anh',$post->ID);
									
									$i = 1;
										foreach($thu_vien_hinh_anh as $slide)
									  {
										?>
											<img src="<?=$slide['full_image_url']?>" data-slider-id="slide-<?=$i?>" data-thumb="<img width='100' src='<?=$slide['thumbnail_image_url']?>' />" style="width:100%"/>
											
										
											<?php
											$i++;
									  }
									?>
								</div>
				
			
			</section>
			<section class="dang_ky_tham_quan">
				<h3 class="text-center text-uppercase">Đăng ký tham quan trường</h3>
				<div class="flex-div border-top-red offset-top-50">
					<div class="col-md-6 form_dang_ky_tham_quan">
						<?php
							$form = get_field('form_dang_ky_tham_quan',$post->ID);
							echo do_shortcode($form);
						?>
					</div>
					<div class="col-md-6 map_co_so"><?=get_field('map_co_so',$post->ID);?></div>
				</div>
			</section>
			<section class="cac_co_so_khac section-30">
				<h3 class="text-center text-uppercase">Các cơ sở khác</h3>
				<?php
					
					$args=array(
										'category' => $categories['value'],
										'exclude' => array($post->ID),
										
										
										);
									$cosokhac = get_posts($args);
									
				?>
				
				<div style="padding:5vw 5vw;">
					<div data-items="1" data-xs-items="1" data-sm-items="2" data-md-items="3" data-thumbs="false"  data-stage-padding="0" data-loop="true" data-margin="0" data-dots="false" data-mouse-drag="true" data-nav="true" data-autoplay="true" class="owl-carousel owl-carousel-1 main-slider qdcslider">
									<?php
									
									$i = 1;
										foreach($cosokhac as $coso)
									  {
										?>
											<div class="coso_item col-md-12">
											<a href="<?=get_permalink($coso->ID)?>"><img src="<?=get_the_post_thumbnail_url($coso->ID,'large')?>" data-slider-id="slide-<?=$i?>" style="width:100%"/></a>
											<div class="text-center">
												<a href="<?=get_permalink($coso->ID)?>"><?=$coso->post_title?></a>
											</div>
											</div>
											<?php
											$i++;
									  }
									?>
								</div>
				</div>
			</section>
			<?php
		}
		else
		{
	 ?>
	 
	 <section class="">
          <div class="">
            <div class="flex-div">
			<?php
					if(!$hide_main_title)
					{
						?>
						<h1 style="text-transform:uppercase;" class="col-md-12"><?=the_title();?></h1>
						<?php
					}
				?>
			
				<div class="block-div">
					<?php
					// Start the loop.
					while ( have_posts() ) : the_post();

						// Include the single post content template.
						get_template_part( 'template-parts/content', 'single' );

						// If comments are open or we have at least one comment, load up the comment template.
						//if ( comments_open() || get_comments_number() ) {
						//	comments_template();
						//}

						
						

						// End of the loop.
					endwhile;
					?>
				</div>
				
				
			</div>
		  </div>
	</section>
	<?php 
	}
	?>
</main>
<!-- Page Content-->



<?php get_footer(); ?>
