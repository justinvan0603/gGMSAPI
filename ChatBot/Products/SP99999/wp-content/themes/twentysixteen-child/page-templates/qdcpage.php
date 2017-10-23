<?php
/**
 * Template Name: QDC Page
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

get_header(); ?>

<?php
?>
 <!-- Page Content-->
<main class="page-content">
     <?php
		if($b_image!=''||$b_color!='')
		{
			?>
			<section class="bg-breadcrumbs text-center text-sm-left txt-white section-sm-237 section-237" style="background-image:url(<?=$b_image?>);background-color: <?=$b_color?>;background-size:cover;">
			 </section>
			<?php
		}
	 ?>
	 <section class="cont-breadcrumbs section-15">
		<div class="shell">
					<?php if (function_exists('breadcrumbs')) breadcrumbs(pll__("Trang chủ")); ?>
				  </div>
	 </section>
	 <section class="section-30 section-md-30">
          <div class="shell">
            <div class="range">
				<h1 style="text-transform:uppercase;" class="cell-md-12"><?=the_title();?></h1>
				<div class="cell-md-4 offset-top-20" style="text-align:justify;">
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
				<div class="cell-md-8 phan-khuc-cong-trinh">
					 <?php
						if(have_posts())
						{
							$term_ids = get_field('qdc_da_thiet_ke_va_thi_cong',$post->ID);
							foreach($term_ids as $tid)
							{
								$args = array(
								//'post_type' => $post_type,
								'post_type' => 'post',
								'posts_per_page' => -1,
								'tax_query' => array(
									array(
										'taxonomy' =>  'category',
										'field'    => 'term_id',
										'terms'    => $tid
										),
									),
								);
								$the_query = new WP_Query( $args);
								if ( $the_query->have_posts() ) {
									while ( $the_query->have_posts() ) {
										$the_query->the_post();
										?>
											<div class="col-sm-6 col-md-6 hinh-phan-khuc offset-top-20" style="">
                          
											  <a href="<?=get_the_permalink()?>" style="position:relative;overflow:hidden;">
													
													<img style="width:100%;max-width:none;" src="<?=the_post_thumbnail_url( 'medium' )?>" alt="<?=$post->post_title?>" />
													<h5 class="txt-white"><?=$post->post_title?></h5>
													<button onclick="location.href = '<?=get_the_permalink()?>';"><?php echo pll__('Xem thêm');?></button>
											  </a>
											  
											</div>
										<?php
									}
									wp_reset_postdata();
								}
							}
						}
						/*
						foreach($hinh_anh_phan_khuc_cong_trinh as $hinh)
                      {
                        ?>	
                        
              					<div class="cell-sm-6 cell-md-6 hinh-phan-khuc offset-top-20">
                          
                          <a href="<?=$hinh['link']?>" style="position:relative;">
                            	<img src="<?=$hinh['image']?>" alt="<?=$hinh['title']?>" />
                            	<h5 class="txt-white"><?=$hinh['title']?></h5>
                            	<button onclick="location.href = '<?=$hinh['link']?>';">Xem thêm</button>
                          </a>
                          
                        </div>
                    	<?php
                      }*/
                    ?>
				</div>
			</div>
		  </div>
	</section>
</main>
<!-- Page Content-->

<?php

get_footer();
