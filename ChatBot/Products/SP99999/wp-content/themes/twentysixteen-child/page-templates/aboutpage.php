<?php
/**
 * Template Name: About Page
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

get_header(); ?>


 <?php
	$breadcrumb_background = ot_get_option('breadcrumb_background',array());
	$b_image = $breadcrumb_background['background-image'];
	$b_color = $breadcrumb_background['background-color']; 
	if(have_posts())
	{
		$background_image = get_field_object('background_image',$post->ID);
		if(isset($background_image)&&$background_image['value']!='')
		{
			$b_image = $background_image['value'];
		}
		$background_color = get_field_object('background_color',$post->ID);
		if(isset($background_color)&&$background_color['value']!='')
		{
			$b_color = $background_color['value'];
		}
		//echo $post-> ID;
		
		
		
		
		//$args = array( 'post_type' => 'attachment', 'posts_per_page' => -1, 'post__in' => $images, 'orderby' => 'post__in' ); 
		//$images2 = get_posts( $args );
		//print_r($images2);
	}
	
					
?>
<!-- Page Content-->
<main class="page-content">
     <?php
		$background_title = get_field_object('background_title',$post->ID);
		$background_description = get_field_object('background_description',$post->ID);
		$background_title_color = get_field_object('background_title_color',$post->ID);
		$background_description_color = get_field_object('background_description_color',$post->ID);
		$background_style = get_field_object('background_style',$post->ID);
		$hide_main_title = get_field_object('hide_main_title',$post->ID);
		
		if($b_image!=''||$b_color!='')
		{
			?>
			<section class="bg-breadcrumbs <?=$background_style['value']?>" style="background-image:url(<?=$b_image?>);background-color: <?=$b_color?>;background-size:cover;">
			<?php
				if(isset($background_title)&&$background_title['value']!='')
				{
					?><h1 style="text-transform:uppercase;color:<?=$background_title_color['value']?>" class="cell-md-6"><?=$background_title['value']?></h1>
					<?php
				}
				if(isset($background_description)&&$background_description['value']!='')
				{
					?><div style="color:<?=$background_description_color['value']?>" class="col-md-6 text-justify"><?=$background_description['value']?></div>
					<?php
				}
			?>
			 </section>
			<?php
		}
	 ?>
	 <section class="cont-breadcrumbs section-15">
		<div class="shell">
					<?php if (function_exists('breadcrumbs')) breadcrumbs(__("Trang chủ")); ?>
				  </div>
	 </section>
	 <section class="">
          <div class="shell">
            <div class="range">
				<?php
					if(!$hide_main_title)
					{
						?>
						<h1 style="text-transform:uppercase;" class="cell-md-12"><?=the_title();?></h1>
						<?php
					}
				?>
				
				<div class="cell-md-12 offset-top-20" style="text-align:justify;">
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
			</div>
		  </div>
	</section>
	<?php
			$gioithieu = get_field('gioi_thieu_content');
			//print_r($gioithieu);
			$gt_idx = 1;
			foreach($gioithieu as $g)
			{
				echo '<section style="background-color:'.$g['background_color'].';" class="section-50 section-md-50" id="section-part-'.$gt_idx.'">';
				if($g['full']=='wrap'){echo '<div class="shell">';}
				echo '<div class="range">';
				$column = $g['column'];
				$lstcolumn = split('_',$column);
				$vitri =$g['vi_tri_noi_dung'];
				$colidx = 1;
				foreach($lstcolumn as $col)
				{
					if($vitri!=$colidx) $style='style="background:url('.wp_get_attachment_image_url($g['hinh_anh'],'large').') no-repeat center center;background-size:cover;"';
					else $style='';
					echo '<div class="col-md-'.$col.'" '.$style.'>';
					if($vitri==$colidx) echo $g['noi_dung'];
					echo '</div>';
					$colidx++;
				}
				echo '</div>';
				if($g['full']=='wrap'){echo '</div>';}
				echo '</section>';
				$gt_idx++;
			}
	?>
</main>
<!-- Page Content-->

<?php

get_footer();
