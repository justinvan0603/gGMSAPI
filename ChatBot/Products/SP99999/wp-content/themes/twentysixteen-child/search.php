<?php
/**
 * The template for displaying search results pages
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

get_header(); ?>


<!-- Page Content-->
<main class="page-content">
     <section class="bg-breadcrumbs text-center text-sm-left txt-white section-sm-55 section-15">
          <div class="shell">
			<?php 
				$searchtext = __('Kết quả tìm kiếm cho từ khóa "').esc_html( get_search_query() ).'"';
				if (function_exists('breadcrumbs')) breadcrumbs(__("Trang chủ"),$searchtext); 
			?>
          </div>
		  
     </section>
	 <section class="section-50 section-md-120 bg-downriver">
          <div class="shell">
            <div class="range">
				<div class="cell-md-8">
					<div class="range">
					<?php if ( have_posts() ) : ?>

						

						<?php
						// Start the Loop.
						while ( have_posts() ) : the_post();

							/*
							 * Include the Post-Format-specific template for the content.
							 * If you want to override this in a child theme, then include a file
							 * called content-___.php (where ___ is the Post Format name) and that will be used instead.
							 */
							get_template_part( 'template-parts/content', get_post_format() );

						// End the loop.
						endwhile;
						?>
						<div class="cell-xs-12 text-center offset-top-55">
							
						  	
						<?php
						// Previous/next page navigation.
						the_posts_pagination( array(
							'prev_text'          => __( 'Previous page', 'twentysixteen' ),
							'next_text'          => __( 'Next page', 'twentysixteen' ),
							'before_page_number' => '',
						) );
						?>
							</div>	
						<?php
					// If no content, include the "No posts found" template.
					else :
						get_template_part( 'template-parts/content', 'none' );

					endif;
					?>
					</div>
				</div>
				<?php get_sidebar('custom'); ?>
			</div>
		  </div>
	</section>
</main>
<!-- Page Content-->



<?php get_footer(); ?>
