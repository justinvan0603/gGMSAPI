<?php
/**
 * The template part for displaying content
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */
 $postid = $post->ID;
 $comment_count = wp_count_comments($postid);
?>
<div class="item offset-top-20 col-md-6">
                    					
                    					<div class="cell-sm-12 cell-md-12">
                                  <div class="image"><a href="<?=get_permalink($postid)?>"><?=get_the_post_thumbnail($post,'large')?></a></div>
                                	<h5 class="offset-top-20"><a href="<?=get_permalink($postid)?>"><?=wp_trim_words($post->post_title,'10','...')?></a></h5>
                                	<div class="info offset-top-10">
                                    	<span class="date"><?php echo get_the_date('d,M,Y',$post)?></span>
                                	</div>
                                  <p class="offset-top-10 excerpt" style="min-height:0;"><?=gsoft_get_excerpt($post,'30','...') ?></p>
                                  
                                <button class="offset-top-20" onclick="location.href = '<?=get_permalink($postid)?>';"><?php echo __('Xem chi tiết');?></button>
                               </div>
</div>