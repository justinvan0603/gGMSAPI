<?php
/**
 * The template for the content bottom widget areas on posts and pages
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

if ( ! is_active_sidebar( 'blog-sidebar' )) {
	return;
}

// If we get this far, we have widgets. Let's do this.
?>
<div class="cell-md-4 offset-md-top-0 offset-top-70">
	<div class="range sidebar">
	<?php if ( is_active_sidebar( 'blog-sidebar' ) ) : ?>
		
			<?php dynamic_sidebar( 'blog-sidebar' ); ?>
		
	<?php endif; ?>
	</div>
</div><!-- .content-bottom-widgets -->
