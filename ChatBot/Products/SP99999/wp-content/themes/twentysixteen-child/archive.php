<?php
/**
 * The template for displaying archive pages
 *
 * Used to display archive-type pages if nothing more specific matches a query.
 * For example, puts together date-based pages if no date.php file exists.
 *
 * If you'd like to further customize these archive views, you may create a
 * new template file for each one. For example, tag.php (Tag archives),
 * category.php (Category archives), author.php (Author archives), etc.
 *
 * @link https://codex.wordpress.org/Template_Hierarchy
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

get_header(); ?>


<!-- Page Content-->
<main class="page-content archive-page offset-top-15">

    <div class="left-content-div col-md-7" style="padding-left:0;">
					
					<?php
					echo "<div class='left-content offset-bottom-15'>";
					echo "<h2 class='text-center' style='font-size:25px;'>".get_the_archive_title()."</h2>";
					echo "<div class='left-content-items offset-top-10'>";
					if ( have_posts() ) : ?>

						

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
						<div class="cleared"></div>
						<div class="cell-xs-12 text-center offset-top-30">
							
						  	
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
					echo "</div>";
					echo "</div>";
					?>
	</div>
				<?php get_sidebar('custom'); ?>
</main>
<!-- Page Content-->
<div class="cleared"></div>
	

<?php get_footer(); ?>
