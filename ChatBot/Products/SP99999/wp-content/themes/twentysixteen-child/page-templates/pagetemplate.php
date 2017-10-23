<?php
/**
 * Template Name: Page Template Full
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */

get_header(); 
?>
<link rel="stylesheet" href="<?php bloginfo( 'stylesheet_directory' ); ?>/html/css/jquery-ui.css" />
<link rel="stylesheet" href="<?php bloginfo( 'stylesheet_directory' ); ?>/html/css/jquery.fancybox.css" />
  
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
<main class="page-content page-template-full">
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
			<section class="bg-breadcrumbs flex-div <?=$background_style['value']?>" style="background-image:url(<?=$b_image?>);background-position:center center;background-color: <?=$b_color?>;background-size:cover;min-height:<?=get_field('background_height',$post->ID)?>vw">
			<div style="z-index:2;" class="col-md-<?=get_field('background_column',$post->ID)?>">
			<?php
				if(isset($background_title)&&$background_title['value']!='')
				{
					?><h1 style="text-transform:uppercase;color:<?=$background_title_color['value']?>" class="text-center"><?=$background_title['value']?></h1>
					<?php
				}
				if(isset($background_description)&&$background_description['value']!='')
				{
					?><div style="color:<?=$background_description_color['value']?>;line-height:30px;" class="text-center offset-top-20"><?=$background_description['value']?></div>
					<?php
				}
			?>
			</div>
			 </section>
			<?php
		}
	 ?>
	 <section class="cont-breadcrumbs section-15">
		<div class="">
					<?php if (function_exists('breadcrumbs')) breadcrumbs(__("Trang chủ")); ?>
				  </div>
	 </section>
	 <section class="section-30 section-md-30">
          <div class="">
            <div class="">
				<?php
					if(!$hide_main_title)
					{
						?>
						<h1 style="text-transform:uppercase;" class="col-md-12"><?=the_title();?></h1>
						<?php
					}
				?>
				<?php
					$noi_dung_chinh = get_field('noi_dung_chinh',$post->ID);
					$row_idx = 1;
					
					if($noi_dung_chinh)
					{
						foreach($noi_dung_chinh as $nd)
						{
							?>
							<section style="padding:<?=$nd['padding']?>vw;background-color:<?=$nd['background_color']?>;background-image:url(<?=$nd['background_image']?>);background-position:center center;background-size:cover;" id = "row-<?=$row_idx?>">
								<?php
														if($nd['show_image_oval'])
														{
															?>
															<style>
															#row-<?=$row_idx?> img{border-radius:50%;}
															</style>
															<?php
														}
													?>
								<?php
									if($nd['title']!='')
									{
										echo '<h3 class="ndc_title text-center" style="padding:2vw;">'.$nd['title'].'</h3>';
									}
									if($nd['description']!='')
									{
										echo '<div class="ndc_des text-center" style="margin-bottom:2vw">'.$nd['description'].'</div>';
									}
									if($nd['show_slide'])
									{
										echo '<div data-items="1" data-md-items="3" data-thumbs="false"  data-stage-padding="0" data-loop="false" data-margin="10" data-dots="true" data-mouse-drag="true" data-nav="false" data-autoplay="true" class="owl-carousel owl-carousel-1 main-slider page-template-slider">';
										//print_r($nd['noi_dung']);
										$col_idx = 1;
										foreach($nd['noi_dung'] as $n)
										{
											?>
											<div class="slide-item-<?=$col_idx?>" id="col-<?=$col_idx?>" style="padding:<?=$n['padding']?>vw;background-color:<?=$n['background_color']?>;background-image:url(<?=$n['background_image']?>);background-position:center center;background-size:cover;">
												<?=$n['text']?>
												<style>
													#row-<?=$row_idx?> #col-<?=$col_idx?> p,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h1,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h2,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h3,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h4,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h5,
													#row-<?=$row_idx?> #col-<?=$col_idx?> a,
													#row-<?=$row_idx?> #col-<?=$col_idx?> li,
													#row-<?=$row_idx?> #col-<?=$col_idx?>{color:<?=$n['text_color']?>}
													#row-<?=$row_idx?> #col-<?=$col_idx?> img{width:<?=$n['image_width']?>;    height: auto;}
													#row-<?=$row_idx?> #col-<?=$col_idx?>{text-align:<?=$n['align']?>}
												</style>
											</div>
											<?php
											$col_idx++;
										}
										echo '</div>';
									}
									else
									{
										echo '<div class="ndc_content '.$nd['display'].'-div" style="flex-direction:'.$nd['direction'].'">';
										//print_r($nd['noi_dung']);
										$col_idx = 1;
										foreach($nd['noi_dung'] as $n)
										{
											?>
											<div class="col-md-<?=$n['column']?>" id="col-<?=$col_idx?>" style="padding:<?=$n['padding']?>vw;background-color:<?=$n['background_color']?>;background-image:url(<?=$n['background_image']?>);background-position:center center;background-size:cover;">
												<?=$n['text']?>
												<style>
													#row-<?=$row_idx?> #col-<?=$col_idx?> p,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h1,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h2,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h3,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h4,
													#row-<?=$row_idx?> #col-<?=$col_idx?> h5,
													#row-<?=$row_idx?> #col-<?=$col_idx?> a,
													#row-<?=$row_idx?> #col-<?=$col_idx?> li,
													#row-<?=$row_idx?> #col-<?=$col_idx?>{color:<?=$n['text_color']?>}
													#row-<?=$row_idx?> #col-<?=$col_idx?> img{width:<?=$n['image_width']?>;    height: auto;}
													#row-<?=$row_idx?> #col-<?=$col_idx?>{text-align:<?=$n['align']?>}
													<?php
														if($nd['display']=='block')
														{
															?>
															#row-<?=$row_idx?> #col-<?=$col_idx?>{width:calc(100% / ( 12 / <?=$n['column']?> ) - <?=$n['padding']?>vw);min-height:380px;overflow:hidden;vertical-align: top;}
															<?php
														}
													?>
												</style>
											</div>
											<?php
											$col_idx++;
										}
										echo '</div>';
									}
									if($nd['show_calendar'])
									{
										$lstevent = ot_get_option('calendar');
										echo '<div class="calendar-panel flex-div">';
										
										?>
											<div class="calendar col-md-6" id="calendar"></div>
											<div class="calendar-content col-md-6 flex-div"></div>
											<script>
													  var disableddates =[];
													  var contentdates = [];
													  
													  <?php
														$i = 0;
														$cur_event = end($lstevent);
														
														$cur_date_str = date('m/d/Y',strtotime(str_replace('/', '-', $cur_event['date'])));
														
														?>
														var currentdate = "<?php echo $cur_date_str ?>";
														console.log(currentdate);
														<?php
														foreach($lstevent as $event)
														{
															$date = $event['date'];
															$content = $event['title'];
															?>
															disableddates[<?=$i?>]="<?=$date?>";
															contentdates["<?php echo date('m/d/Y',strtotime(str_replace('/', '-', $date)))?>"] = "<?=$content?>";
															<?php
															$i++;
														}
													  ?>
													  console.log(contentdates);
													function DisableSpecificDates(date) {
														var string = jQuery.datepicker.formatDate('yy-mm-dd', date);
														return [disableddates.indexOf(string) != -1];
													  }
													function SelectDate(date,evnt)
													{
														jQuery('.calendar-content').html('<span style="margin:auto;flex:1;">'+contentdates[""+date+""]+'</span>');
													}
													  jQuery( function() {
														jQuery( "#calendar" ).datepicker({
															beforeShowDay: DisableSpecificDates,
															onSelect:SelectDate,
															defaultDate:currentdate
														});
														
													  } );
													  jQuery('.calendar-content').html('<span style="margin:auto;flex:1;"><?=$cur_event['title']?></span>');
											</script>
										<?php
										echo '</div>';
									}
									
								?>
							</section>
							
							<?php
							$row_idx++;
						}
					}
				?>
			</div>
		  </div>
	</section>
	<section class="" style="text-align:justify;overflow:hidden;">
		<div class="col-md-12 offset-top-20" style="text-align:justify;overflow:hidden;">
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
	</section>
</main>
<!-- Page Content-->
<?php get_footer();