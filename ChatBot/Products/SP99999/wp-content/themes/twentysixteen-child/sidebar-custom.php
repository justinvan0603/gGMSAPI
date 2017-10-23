<?php
/**
 * The template for the content bottom widget areas on posts and pages
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */
?>
<div class="right-content col-md-5" style="padding-right:0;">
			<img src="<?php echo home_url()?>/wp-content/uploads/2017/05/event2.gif" />
			<div class="video-anninh offset-top-15">
				<h2 class='title-navi'>Video</h2>
				<div class='video-items'>
				<?php
					$right_video = ot_get_option('right_video');
					$i=1;
					foreach($right_video as $v)
					{
						$open='';
						if($i==1) $open = 'open';
						echo "<div class='video-item ".$open."  offset-top-10'>";
						echo "<h4><i class='fa fa-youtube-play'></i>".$v['title']."</h4>";
						echo "<video height='240' width='100%' controls src='".$v['video']."' class='video-content'>Your browser does not support the video tag.</video>";
						echo "</div>";
						$i++;
					}
				?>
				</div>
			</div>
			<div class="image-anninh offset-top-15">
				<h2 class='title-navi'>Hình ảnh</h2>
				<div class='owl-carousel image-items offset-top-10' data-items="1" data-stage-padding="0" data-loop="false" data-animateOut="fadeOut" data-animateIn="fadeIn" data-margin="0" data-dots="false" data-mouse-drag="true" data-nav="true" data-autoplay="true">
				<?php
					$right_image = ot_get_option('right_image');
					$i=1;
					foreach($right_image as $v)
					{
						echo "<div class='image-item'>";
						echo "<img width='473' height='314' src='".$v['image']['background-image']."' />";
						echo "</div>";
						$i++;
					}
				?>
				</div>
			</div>
			<div class="content-anninh offset-top-15">
				<?php
				$right_content = ot_get_option('right_content');
				$j = 1;
				foreach($right_content as $right)
				{
					echo "<div class='right-content right-content-".$j." offset-bottom-15' id='right-content-".$i."'>";
					echo "<h2 class='title-navi'>".$right['title']."</h2>";
					echo "<div class='right-content-items offset-top-10'>";
					$cat_id = $right['category'];
					$tin = get_posts(array('post_type'      => 'post',
						'posts_per_page' => 8,
						'post_status'    => 'publish','order'          => 'DESC',
						'orderby'        => 'date','category'=>$cat_id));
					
					foreach($tin as $t)
					{
						
							echo "<div class='right-content-item offset-bottom-10'>";
							echo "<h4><a title='".$t->post_title."' href='".get_permalink($t)."'><i class='fa fa-newspaper-o' aria-hidden='true'></i>".wp_trim_words($t->post_title,20,'...')."</a></h4>";
							echo "<p>".wp_trim_words(wp_strip_all_tags($t->post_content),20,'...')."</p>";
							echo "</div>";
						
						
					}
					echo "</div>";
					echo "</div>";
					$j++;
				}
			?>
			</div>
		</div>