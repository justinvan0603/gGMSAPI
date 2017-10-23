<?php
/**
 * The template for displaying all single posts and attachments
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

	get_header(); 

?>

<!-- Page Content-->
<main class="page-content single-page offset-top-15">

    <div class="left-content-div col-md-7" style="padding-left:0;">
					
					<?php
					echo "<div class='left-content offset-bottom-15'>";
					echo "<h2 class='text-center' style='font-size:25px;'>".get_the_title()."</h2>";
					echo "<div class='left-content-items offset-top-10'>";
					// Start the loop.
					while ( have_posts() ) : the_post();

						// Include the single post content template.
						get_template_part( 'template-parts/content', 'single' );

						// If comments are open or we have at least one comment, load up the comment template.
						if ( comments_open() || get_comments_number() ) {
							comments_template();
						}

						
						

						// End of the loop.
					endwhile;
					echo "</div>";
					echo "</div>";
					
					?>
					
	</div>
				<?php get_sidebar('custom'); ?>
</main>
<!-- Page Content-->
<div class="cleared"></div>


<?php get_footer(); ?>
