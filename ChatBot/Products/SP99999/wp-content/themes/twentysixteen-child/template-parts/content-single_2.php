<?php
/**
 * The template part for displaying single posts
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */
?>


	<h1 class="text-center text-uppercase">	<?echo get_the_title() ?></h1>
	<div class="block-div button-panel">
	<div class="flex-div button-panel-content">
		<div class="col-md-4 text-left"><a href="javascript:history.go(-1);"><i class="fa fa-arrow-circle-o-left"></i> Quay lại</a></div>
		<div class="col-md-4 text-center">Ngày <?=get_the_date('d-m-Y')?></div>
		<div class="col-md-4 text-right"><a target="_blank" href="http://www.facebook.com/sharer/sharer.php?u=<?=get_permalink()?>" class="facebook-btn"><i class="fa fa-facebook"></i> Share</a>
		<a href="#" onclick="printThis()" class="print-btn"><i class="fa fa-print" aria-hidden="true"></i></a>
		</div>
	</div>
	</div>
<div class="content-page-blog">
<div class="blog-post block-round" style="text-align:justify;">
	
	<div class="meta offset-top-20 text-italic" >
                      <!--<div class="block-time reveal-xs-inline-block"><span class="icon icon-emerland icon-md mdi-calendar-clock text-middle"></span><span class="text-middle"><?echo get_the_date('d-m-Y')?></span></div>-->
                      
                    </div>
					<p class="offset-top-26"></p>
		<?php
			the_content();

			wp_link_pages( array(
				'before'      => '<div class="page-links"><span class="page-links-title">' . __( 'Pages:', 'twentysixteen' ) . '</span>',
				'after'       => '</div>',
				'link_before' => '<span>',
				'link_after'  => '</span>',
				'pagelink'    => '<span class="screen-reader-text">' . __( 'Page', 'twentysixteen' ) . ' </span>%',
				'separator'   => '<span class="screen-reader-text">, </span>',
			) );

			if ( '' !== get_the_author_meta( 'description' ) ) {
				get_template_part( 'template-parts/biography' );
			}
		?>

	
	<!--
	<footer class="entry-footer">
		<?php twentysixteen_entry_meta(); ?>
		<?php
			edit_post_link(
				sprintf(
					/* translators: %s: Name of current post */
					__( 'Edit<span class="screen-reader-text"> "%s"</span>', 'twentysixteen' ),
					get_the_title()
				),
				'<span class="edit-link">',
				'</span>'
			);
		?>
	</footer>--><!-- .entry-footer -->
</div>
<?php if(has_tag()){?>
<div class="block-round offset-top-30 section-30 inset-xs-left-30 inset-xs-right-30 inset-left-10 inset-right-10">
	<?php
		 the_tags();
	?>
</div>
<?php } ?>

</div>