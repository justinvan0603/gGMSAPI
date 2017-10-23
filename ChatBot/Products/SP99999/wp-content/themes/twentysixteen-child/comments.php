<?php
/**
 * The template for displaying comments
 *
 * The area of the page that contains both current comments
 * and the comment form.
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

/*
 * If the current post is protected by a password and
 * the visitor has not yet entered the password we will
 * return early without loading the comments.
 */
if ( post_password_required() ) {
	return;
}
?>



	<?php if ( have_comments() ) : ?>
	<div class="block-round offset-top-30 section-30 inset-xs-left-30 inset-xs-right-30 inset-left-10 inset-right-10">
                  <h6 class="font-sec">
			<?php
				$comments_number = get_comments_number();
				if ( 1 === $comments_number ) {
					/* translators: %s: post title */
					/*printf( _x( '1 Bình luận về &ldquo;%s&rdquo;', 'comments title', 'twentysixteen' ), get_the_title() );*/
					 printf( _x( '1 Bình luận %s', 'comments title', 'twentysixteen' ), '' );
				} else {
					printf(
						/* translators: 1: number of comments, 2: post title */
						_nx(
							'%1$s Bình luận %2$s',
							'%1$s Bình luận %2$s',
							$comments_number,
							'comments title',
							'twentysixteen'
						),
						number_format_i18n( $comments_number ),
						''
						/*get_the_title()*/
					);
				}
			?>
		</h6>
                  <div class="divider-lg offset-top-10"></div>
                  
       
		

		<?php the_comments_navigation(); ?>

		
			<?php wp_list_comments('type=comment&callback=format_comment'); ?>
		

		<?php the_comments_navigation(); ?>
 </div>
	<?php endif; // Check for have_comments(). ?>

	<?php
		// If comments are closed and there are comments, let's leave a little note, shall we?
		if ( ! comments_open() && get_comments_number() && post_type_supports( get_post_type(), 'comments' ) ) :
	?>
		<p class="no-comments"><?php _e( 'Comments are closed.', 'twentysixteen' ); ?></p>
	<?php endif; ?>
	<div class="block-round offset-top-30 section-30 inset-xs-left-30 inset-xs-right-30 text-center text-sm-left inset-left-10 inset-right-10">
	<?php
		$fields = array(
			'author'=> '<div class="cell-sm-6 offset-top-40"><div class="form-group">
                          <label for="author" class="form-label">Tên</label>
                          <input id="author" type="text" name="author" data-constraints="@Required" class="form-control" />
                        </div></div>',
			'email'=> '<div class="cell-sm-6 offset-top-40"><div class="form-group">
                          <label for="email" class="form-label">Email</label>
                          <input id="email" type="email" name="email" data-constraints="@Required @Email" class="form-control" />
                        </div></div>',
		);
		$comment_field = '<div class="cell-xs-12 offset-top-40"><div class="form-group">
                          <label for="comment" class="form-label">Bình luận</label>
                          <textarea id="comment" name="comment" data-constraints="@Required" class="form-control"></textarea>
                        </div></div>';
		comment_form( array(
			'title_reply_before' => '<h6 id="reply-title" class="font-sec">',
			'title_reply_after'  => '</h6><div class="divider-lg offset-top-10"></div>',
			'must_log_in'=>'<p style="margin-top:10px;" class="must-log-in">' .  sprintf( __( 'You must be <a href="%s">logged in</a> to post a comment.' ), wp_login_url( apply_filters( 'the_permalink', get_permalink( ) ) ) ) . '</p>',
			//'class_form'=> 'comment-form rd-mailform',
			'class_submit'=> 'submit btn btn-primary btn-sm-2 btn-shadow btn-rect btn-icon btn-icon-left offset-top-18'
			//'fields' => $fields,
			//'comment_field'=> $comment_field,
			
			
			
		) );
	?>
	</div>
<style>
	.btn.btn-primary{padding:5px 10px;font-size:14px;}
	#commentform label{display:inline-block;width:100px;}
</style>
<?php

    function format_comment($comment, $args, $depth) {
    
       $GLOBALS['comment'] = $comment; ?>
       
        <div class="media-variant-4 offset-top-30 <?=$comment->comment_parent==0?'':'inset-sm-left-70'?>" id="li-comment-<?php comment_ID() ?>">
             <!--<div class="media-left round"> <?php echo get_avatar( $comment, 70 ); ?></div>-->
                    <div class="media-body">
                      <p class="big text-primary fw-b"><?php comment_author(); ?></p>
                      <p class="offset-top-0 txt-italic txt-gray"><?echo human_time_diff(get_comment_time('U'), current_time('timestamp')).' trước'?></p>
                      <p class="offset-top-7"><?php comment_text(); ?></p>
					  
					  <?php comment_reply_link(array_merge( $args, 
						array(
							'depth' => $depth, 
							'max_depth' => $args['max_depth'],
							'reply_text' => '<span class="fa fa-reply"></span><span>Trả lời</span>',
							))) ?>
					  
                    </div>
					
           
       </div>
<?php } ?>
