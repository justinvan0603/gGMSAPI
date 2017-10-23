<?php
/**
 * Template Name: Branch Page
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

get_header(); ?>


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
<main class="page-content cosopage" style="position:relative;">
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
			 </section>
			<?php
		}
	 ?>
	 <section class="cont-breadcrumbs section-15">
		<div class="shell">
					<?php if (function_exists('breadcrumbs')) breadcrumbs(__("Trang chủ")); ?>
				  </div>
	 </section>
	 <section class="cac_co_so_section">
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
				
				<div class="col-md-12 border-top-red section-50" style="text-align:justify;position:inherit;background-color:#e1e1e1;">
					<div class="col-md-6">
						<?php
					// Start the loop.
					while ( have_posts() ) : the_post();

						// Include the page content template.
						get_template_part( 'template-parts/content', 'page' );

						// If comments are open or we have at least one comment, load up the comment template.
						if ( comments_open() || get_comments_number() ) {
							comments_template();
						}

						// End of the loop.
					endwhile;
					?>
					</div>
					<div class="col-md-6 cac_co_so">
						<h1 class="cac_co_so_title">Các cơ sở trực thuộc</h1>
						<?php
							$cac_co_so = get_field('cac_co_so',$post->ID);
							$args=array(
										'category' => $cac_co_so,
										);
							$coso = get_posts($args);
							foreach($coso as $cs)
							{
								?>
								<div class="co_so_item flex-div">
									<div class="col-md-4 co_so_image">
										<a href="<?=get_permalink($cs->ID)?>"><img src="<?=get_the_post_thumbnail_url($cs->ID,'thumbnail')?>" /></a>
									</div>
									<div class="col-md-8 co_so_content">
										<div class="co_so_name"><a href="<?=get_permalink($cs->ID)?>"><?=$cs->post_title?></a></div>
										<div class="co_so_des"><?=wp_trim_words($cs->post_content,'40','...')?></div>
									</div>
								</div>
								<?php
							}
						?>
					</div>
				</div>
			</div>
		  </div>
	</section>
	
</main>
<!-- Page Content-->

<?php

get_footer();
