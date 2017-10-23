<?php
/**
 * The template part for displaying a message that posts cannot be found
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */
?>
<div class="cell-xs-12 offset-top-30 page-404">
                    <div class="shell">
						<h3 class="divider-sm divider-sm-mod-1"><?php _e( 'Nothing Found', 'twentysixteen' ); ?></h3>
					<p class="big offset-top-43">404</p>
					<h1 class="page-title"></h1>
					<?php if ( is_home() && current_user_can( 'publish_posts' ) ) : ?>

			<p class="offset-top-17"><?php printf( __( 'Ready to publish your first post? <a href="%1$s">Get started here</a>.', 'twentysixteen' ), esc_url( admin_url( 'post-new.php' ) ) ); ?></p>

		<?php elseif ( is_search() ) : ?>

			<p class="offset-top-17"><?php _e( 'Sorry, but nothing matched your search terms. Please try again with some different keywords.', 'twentysixteen' ); ?></p>
			

		<?php else : ?>

			<p class="offset-top-17"><?php _e( 'It seems we can&rsquo;t find what you&rsquo;re looking for. Perhaps searching can help.', 'twentysixteen' ); ?></p>
			

		<?php endif; ?>
		<div class="btn-group-custom-3 offset-top-60"><a href="/" class="btn btn-sm btn-primary btn-icon btn-icon-left btn-shadow btn-rect offset-top-30"><span class="icon icon-md mdi-arrow-left-bold"></span><span> Về trang chủ</span></a><a href="/lien-he" class="btn btn-sm btn-white btn-icon btn-icon-left btn-shadow btn-rect offset-top-30"><span class="icon icon-md mdi-email-outline"></span><span>Liên hệ với chúng tôi</span></a></div>
					</div>
                  </div>
				  