<?php
/**
 * Twenty Sixteen Child functions and definitions
 *
 * Set up the theme and provides some helper functions, which are used in the
 * theme as custom template tags. Others are attached to action and filter
 * hooks in WordPress to change core functionality.
 *
 * When using a child theme you can override certain functions (those wrapped
 * in a function_exists() call) by defining them first in your child theme's
 * functions.php file. The child theme's functions.php file is included before
 * the parent theme's file, so the child theme functions would be used.
 *
 * @link https://codex.wordpress.org/Theme_Development
 * @link https://codex.wordpress.org/Child_Themes
 *
 * Functions that are not pluggable (not wrapped in function_exists()) are
 * instead attached to a filter or action hook.
 *
 * For more information on hooks, actions, and filters,
 * {@link https://codex.wordpress.org/Plugin_API}
 *
 * @package WordPress
 * @subpackage Twenty_Sixteen
 * @since Twenty Sixteen 1.0
 */
//add style in parent theme
function my_theme_enqueue_styles() {

    $parent_style = 'parent-style'; // This is 'twentyfifteen-style' for the Twenty Fifteen theme.

    wp_enqueue_style( $parent_style, get_template_directory_uri() . '/style.css' );
    wp_enqueue_style( 'child-style',
        get_stylesheet_directory_uri() . '/style.css',
        array( $parent_style ),
        wp_get_theme()->get('Version')
    );
}
//add_action( 'wp_enqueue_scripts', 'my_theme_enqueue_styles' );
//add style in parent theme

//register widget
// Creating the new post widget 
class New_Post_widget extends WP_Widget {
	function __construct() {
		parent::__construct(
		// Base ID of your widget
		'New_Post_widget', 

		// Widget name will appear in UI
		__('01. New Post widget', 'thinhnq'), 

		// Widget description
		array( 'description' => __( 'GSOFT New Post widget', 'thinhnq' ), ) 
		);
	}

	// Creating widget front-end
	// This is where the action happens
	public function widget( $args, $instance ) {
		$title = apply_filters( 'widget_title', $instance['title'] );
		$category = $instance['category'];
		$number = $instance['number'];
		// before and after widget arguments are defined by themes
		echo "<div class='cell-sm-12 cell-md-12 offset-top-30 offset-sm-top-60 cell-md-push-3'>";
		//echo $args['before_widget'];
		if ( ! empty( $title ) )
		echo $args['before_title'] . $title . $args['after_title'];

		// This is where you run the code and display the output
		//echo __( 'Hello, World!', 'thinhnq' );
		if($category!='-1')
		{
			$args = array(
					//'post_type' => $post_type,
					'post_type' => 'post',
					'posts_per_page' => $number,
					'tax_query' => array(
						array(
							'taxonomy' =>  'category',
							'field'    => 'term_id',
							'terms'    => $category
							),
						),
					);
			//print_r($args);
			$the_query = new WP_Query( $args);
			if ( $the_query->have_posts() ) {
								//echo '<ul class="post_type_list_children">';
								while ( $the_query->have_posts() ) {
									$the_query->the_post();
									//print_r($post);
									echo '<div class="reveal-inline-block offset-top-20"><a href="' . get_the_permalink() .'" class="fw-b big">' . get_the_title() .'</a></div>';
									echo '<div class="meta">
									  <div class="block-time reveal-xs-inline-block"><span class="icon icon-emerland icon-xs mdi-calendar-clock text-middle"></span><span class="text-middle">'.human_time_diff(get_the_time('U',$post), current_time('timestamp')).' trước'.'</span></div>
									  <div class="block-author reveal-xs-inline-block"><span class="icon icon-emerland icon-xs mdi-account-outline text-middle"></span>'.get_the_author_meta('login',$post->post_author).'</div>
									</div>';
									echo '<div class="divider-lg-3 offset-top-26"></div>';
									//echo '<a class="post" href="' . get_the_permalink() .'" rel="bookmark">' . get_the_title() .'<img src="'.get_template_directory_uri().'/images/new.gif"/></a>';
									//echo '</li>';
								}
								//echo '</ul>';
								wp_reset_postdata();
							}
		}
		// end output
		//echo $args['after_widget'];
		echo "</div>";
	}
			
	// Widget Backend 
	public function form( $instance ) {
		//default var
		if ( isset( $instance[ 'title' ] ) ) {
			$title = $instance[ 'title' ];
		}
		else {
			$title = __( 'New Post', 'thinhnq' );
		}
		//other var
		if ( isset( $instance[ 'category' ] ) ) {
			$category = $instance[ 'category' ];
		}
		else {
			$category = '-1';
		}
		
		if ( isset( $instance[ 'number' ] ) ) {
			$number = $instance[ 'number' ];
		}
		else {
			$number = '0';
		}
		//print_r($category);
		// Widget admin form
		?>
		<p>
		<label for="<?php echo $this->get_field_id( 'title' ); ?>"><?php _e( 'Title:' ); ?></label> 
		<input class="widefat" id="<?php echo $this->get_field_id( 'title' ); ?>" name="<?php echo $this->get_field_name( 'title' ); ?>" type="text" value="<?php echo esc_attr( $title ); ?>" />
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'category' ); ?>"><?php _e( 'Category:' ); ?></label> 
		<?php
			$args = array(
			'show_option_none' => __( 'Select category' ),
			'show_count'       => 1,
			'orderby'          => 'name',
			'echo'             => 0,
			'id'=>$this->get_field_id( 'category' ),
			'name'=>$this->get_field_name( 'category' ),
			'show_count'=>true,
			'hide_empty'=>true,
			'class'=> 'widefat',
			'selected'=>$category
			);
			$select  = wp_dropdown_categories( $args );
			echo $select;
		?>
		
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'number' ); ?>"><?php _e( 'Post number:' ); ?></label> 
		<input class="widefat" id="<?php echo $this->get_field_id( 'number' ); ?>" name="<?php echo $this->get_field_name( 'number' ); ?>" type="text" value="<?php echo esc_attr( $number ); ?>" />
		</p>
		<?php 
	}
		
	// Updating widget replacing old instances with new
	public function update( $new_instance, $old_instance ) {
		$instance = array();
		$instance['title'] = ( ! empty( $new_instance['title'] ) ) ? strip_tags( $new_instance['title'] ) : '';
		$instance['category'] = ( ! empty( $new_instance['category'] ) ) ? strip_tags( $new_instance['category'] ) : '-1';
		$instance['number'] = ( ! empty( $new_instance['number'] ) ) ? strip_tags( $new_instance['number'] ) : '0';
		return $instance;
	}
} // Class wpb_widget ends here
//End New post widget

// Creating the HTML Widget 
class HTML_widget extends WP_Widget {
	function __construct() {
		parent::__construct(
		// Base ID of your widget
		'HTML_widget', 

		// Widget name will appear in UI
		__('02. HTML widget', 'thinhnq'), 

		// Widget description
		array( 'description' => __( 'GSOFT HTML widget', 'thinhnq' ), ) 
		);
	}

	// Creating widget front-end
	// This is where the action happens
	public function widget( $args, $instance ) {
		$title = apply_filters( 'widget_title', $instance['title'] );
		$html = $instance['html'];
		$type = $instance['type'];
		$class = $instance['class'];
		//print_r('----'.$args['before_widget']);
		// before and after widget arguments are defined by themes
		echo "<div class='cell-sm-12 cell-md-12 offset-top-30 offset-sm-top-60 cell-md-push-3 ".$class."'>";
		
		if ( ! empty( $title ) )
		echo $args['before_title'] . $title . $args['after_title'];

		// This is where you run the code and display the output
		//echo __( 'Hello, World!', 'thinhnq' );
		echo '<div class="reveal-inline-block offset-top-20"></div>';
		switch($type)
		{
			case '0':
			{
				echo $html;
				break;
			}
			case '1'://shortcode
			{
				echo do_shortcode($html);
				break;
			}
			case '2'://fanpage
			{
				echo '<div id="fb-root"></div>
						<script>(function(d, s, id) {
						  var js, fjs = d.getElementsByTagName(s)[0];
						  if (d.getElementById(id)) return;
						  js = d.createElement(s); js.id = id;
						  js.src = "//connect.facebook.net/vi_VN/all.js#xfbml=1&appId=340260019435091";
						  fjs.parentNode.insertBefore(js, fjs);
						}(document, "script", "facebook-jssdk"));</script>
						<div class="fb-page" data-href="'.$html.'" data-tabs="timeline" data-height="130" data-small-header="false" data-adapt-container-width="true" data-hide-cover="false" data-show-facepile="false"><blockquote cite="'.$html.'" class="fb-xfbml-parse-ignore"><a href="'.$html.'">GOBRANDING</a></blockquote></div>
						';
			}
		}

		// end output
		//echo $args['after_widget'];
		echo "</div>";
		//echo "</div>";
	}
			
	// Widget Backend 
	public function form( $instance ) {
		//default var
		if ( isset( $instance[ 'title' ] ) ) {
			$title = $instance[ 'title' ];
		}
		else {
			$title = __( 'HTML', 'thinhnq' );
		}
		//other var
		if ( isset( $instance[ 'html' ] ) ) {
			$html = $instance[ 'html' ];
		}
		else {
			$html = '';
		}
		
		if ( isset( $instance[ 'type' ] ) ) {
			$type = $instance[ 'type' ];
		}
		else {
			$type = '0';
		}
		if ( isset( $instance[ 'class' ] ) ) {
			$class = $instance[ 'class' ];
		}
		else {
			$class = '';
		}
		
		//print_r($html);
		// Widget admin form
		?>
		<p>
		<label for="<?php echo $this->get_field_id( 'title' ); ?>"><?php _e( 'Title:' ); ?></label> 
		<input class="widefat" id="<?php echo $this->get_field_id( 'title' ); ?>" name="<?php echo $this->get_field_name( 'title' ); ?>" type="text" value="<?php echo esc_attr( $title ); ?>" />
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'html' ); ?>"><?php _e( 'HTML:' ); ?></label> 
		<?php
			echo wp_editor( $html ,$this->get_field_id( 'html' ),array(
				'textarea_name'=> $this->get_field_name( 'html' ),
				'drag_drop_upload'=>true,
				'editor_class'=>'widefat',
				'tinymce'=>false
			));
		?>
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'type' ); ?>"><?php _e( 'Type:' ); ?></label> 
		<select class="widefat" id="<?php echo $this->get_field_id( 'type' ); ?>" name="<?php echo $this->get_field_name( 'type' ); ?>" value="<?php echo esc_attr( $type ); ?>" >
			<option value="0" <?php echo $type=='0'?'selected':'' ?> >Normal</option>
			<option value="1" <?php echo $type=='1'?'selected':'' ?>>Shortcode</option>
			<option value="2" <?php echo $type=='2'?'selected':'' ?>>Fanpage</option>
		</select>
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'class' ); ?>"><?php _e( 'Class:' ); ?></label> 
		<input class="widefat" id="<?php echo $this->get_field_id( 'class' ); ?>" name="<?php echo $this->get_field_name( 'class' ); ?>" type="text" value="<?php echo esc_attr( $class ); ?>" />
		</p>
		<?php 
	}
		
	// Updating widget replacing old instances with new
	public function update( $new_instance, $old_instance ) {
		$instance = array();
		$instance['title'] = ( ! empty( $new_instance['title'] ) ) ? strip_tags( $new_instance['title'] ) : '';
		$instance['html'] = ( ! empty( $new_instance['html'] ) ) ? $new_instance['html'] : '';
		$instance['type'] = ( ! empty( $new_instance['type'] ) ) ? strip_tags( $new_instance['type'] ) : '0';
		$instance['class'] = ( ! empty( $new_instance['class'] ) ) ? strip_tags( $new_instance['class'] ) : '';
		return $instance;
	}
} // Class wpb_widget ends here
//End HTML widget
// Creating popular page widget 
class Popular_widget extends WP_Widget {
	function __construct() {
		parent::__construct(
		// Base ID of your widget
		'Popular_widget', 

		// Widget name will appear in UI
		__('03. Popular widget', 'thinhnq'), 

		// Widget description
		array( 'description' => __( 'GSOFT Popular widget', 'thinhnq' ), ) 
		);
	}

	// Creating widget front-end
	// This is where the action happens
	public function widget( $args, $instance ) {
		$title = apply_filters( 'widget_title', $instance['title'] );
		$lstpage = $instance['lstpage'];
		$posttype = $instance['posttype'];
		$showtime = $instance['showtime'];
		$showauthor = $instance['showauthor'];
		$showcontent= $instance['showcontent'];
		// before and after widget arguments are defined by themes
		echo "<div class='cell-sm-12 cell-md-12 offset-top-30 offset-sm-top-60 cell-md-push-3'>";
		//echo $args['before_widget'];
		if ( ! empty( $title ) )
		echo $args['before_title'] . $title . $args['after_title'];

		// This is where you run the code and display the output
		//echo __( 'Hello, World!', 'thinhnq' );
		if(!empty($lstpage))
		{
			//print_r($lstpage);
			foreach($lstpage as $idpage)
			{
				$page_obj = get_post($idpage);
				//print_r($page_obj);
				if(!empty($page_obj))
				{
					//setup_postdata( $page_obj );
					echo '<div class="reveal-inline-block offset-top-20"><a href="' . get_permalink($page_obj) .'" class="fw-b big">' . $page_obj->post_title .'</a></div>';
									echo '<div class="meta">';
									if($showtime=='1'){
									  echo '<div class="block-time reveal-xs-inline-block"><span class="icon icon-emerland icon-xs mdi-calendar-clock text-middle"></span><span class="text-middle">'.get_the_date('d-m-Y',$page_obj).'</span></div>';}
									if($showauthor=='1'){
									  echo '<div class="block-author reveal-xs-inline-block"><span class="icon icon-emerland icon-xs mdi-account-outline text-middle"></span>'.get_the_author_meta('login',$page_obj->post_author).'</div>';}
									echo'</div>';
									echo '<div class="content">';
									if($showcontent=='1'){
									echo '<div class="block-content reveal-xs-inline-block"><span class="text-middle">'.gsoft_get_excerpt($page_obj,'20','...').'</span></div>';}
									echo '</div>';
									echo '<div class="divider-lg-3 offset-top-26"></div>';
				}
				
			}
			//wp_reset_postdata();
		}
		// end output
		//echo $args['after_widget'];
		echo "</div>";
	}
			
	// Widget Backend 
	public function form( $instance ) {
		//default var
		if ( isset( $instance[ 'title' ] ) ) {
			$title = $instance[ 'title' ];
		}
		else {
			$title = __( 'New Post', 'thinhnq' );
		}
		//other var
		if ( isset( $instance[ 'posttype' ] ) ) {
			$posttype = $instance[ 'posttype' ];
		}
		else {
			$posttype = 'post';
		}
		if ( isset( $instance[ 'lstpage' ] ) ) {
			$lstpage = $instance[ 'lstpage' ];
		}
		else {
			$lstpage = array();
		}
		if ( isset( $instance[ 'showtime' ] ) ) {
			$showtime = $instance[ 'showtime' ];
		}
		else {
			$showtime = '0';
		}
		if ( isset( $instance[ 'showauthor' ] ) ) {
			$showauthor = $instance[ 'showauthor' ];
		}
		else {
			$showauthor = '0';
		}
		if ( isset( $instance[ 'showcontent' ] ) ) {
			$showcontent = $instance[ 'showcontent' ];
		}
		else {
			$showcontent = '0';
		}
		
		//print_r($category);
		// Widget admin form
		?>
		<p>
		<label for="<?php echo $this->get_field_id( 'title' ); ?>"><?php _e( 'Title:' ); ?></label> 
		<input class="widefat" id="<?php echo $this->get_field_id( 'title' ); ?>" name="<?php echo $this->get_field_name( 'title' ); ?>" type="text" value="<?php echo esc_attr( $title ); ?>" />
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'lstpage' ); ?>"><?php _e( 'Select Pages:' ); ?></label> 
		<?php
			$allpage = get_posts(array('post_type' => $posttype,
	'post_status' => 'publish','orderby'=>'menu_order','order'=>'ASC','posts_per_page'=>-1));
			echo "<select style='width:100%' name='".$this->get_field_name( 'lstpage' )."[]' id='".$this->get_field_id( 'lstpage' )."' multiple>";
			foreach($allpage as $p)
			{
				echo "<option value='".$p->ID."' ".(in_array( $p->ID, $lstpage) ? 'selected="selected"' : '').">".$p->post_title."</option>";
			}
			echo "</select>";
		?>
		
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'posttype' ); ?>"><?php _e( 'Post type:' ); ?></label> 
		<?php
			echo "<select style='width:100%' name='".$this->get_field_name( 'posttype' )."' id='".$this->get_field_id( 'posttype' )."'>";
			echo "<option value='post' ".($posttype=='post'?'selected':'').">Post</option>";
			echo "<option value='page' ".($posttype=='page'?'selected':'').">Page</option>";
			echo "</select>";
		?>
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'showtime' ); ?>"><?php _e( 'Show date:' ); ?></label> 
		<?php
			echo "<select style='width:100%' name='".$this->get_field_name( 'showtime' )."' id='".$this->get_field_id( 'showtime' )."'>";
			echo "<option value='0' ".($showtime=='0'?'selected':'').">No</option>";
			echo "<option value='1' ".($showtime=='1'?'selected':'').">Yes</option>";
			echo "</select>";
		?>
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'showauthor' ); ?>"><?php _e( 'Show Author:' ); ?></label> 
		<?php
			echo "<select style='width:100%' name='".$this->get_field_name( 'showauthor' )."' id='".$this->get_field_id( 'showauthor' )."'>";
			echo "<option value='0' ".($showauthor=='0'?'selected':'').">No</option>";
			echo "<option value='1' ".($showauthor=='1'?'selected':'').">Yes</option>";
			echo "</select>";
		?>
		</p>
		<p>
		<label for="<?php echo $this->get_field_id( 'showcontent' ); ?>"><?php _e( 'Show Content:' ); ?></label> 
		<?php
			echo "<select style='width:100%' name='".$this->get_field_name( 'showcontent' )."' id='".$this->get_field_id( 'showcontent' )."'>";
			echo "<option value='0' ".($showcontent=='0'?'selected':'').">No</option>";
			echo "<option value='1' ".($showcontent=='1'?'selected':'').">Yes</option>";
			echo "</select>";
		?>
		</p>
		<?php 
	}
		
	// Updating widget replacing old instances with new
	public function update( $new_instance, $old_instance ) {
		$instance = array();
		$instance['title'] = ( ! empty( $new_instance['title'] ) ) ? strip_tags( $new_instance['title'] ) : '';
		$instance['lstpage'] = ( ! empty( $new_instance['lstpage'] ) ) ? (esc_sql( $new_instance['lstpage'])) : array();
		$instance['posttype'] = ( ! empty( $new_instance['posttype'] ) ) ? strip_tags( $new_instance['posttype'] ) : 'post';
		$instance['showtime'] = ( ! empty( $new_instance['showtime'] ) ) ? strip_tags( $new_instance['showtime'] ) : '0';
		$instance['showauthor'] = ( ! empty( $new_instance['showauthor'] ) ) ? strip_tags( $new_instance['showtime'] ) : '0';
		$instance['showcontent'] = ( ! empty( $new_instance['showcontent'] ) ) ? strip_tags( $new_instance['showtime'] ) : '0';
		return $instance;
	}
} // Class wpb_widget ends here
//End New post widget
// Register and load the widget
function wpb_load_widget() {
	register_widget( 'New_Post_widget' );
	register_widget( 'HTML_widget' );
	register_widget( 'Popular_widget' );
}
add_action( 'widgets_init', 'wpb_load_widget' );

//register widget

//other function
//get label for option tree
function get_setting_label_by_id( $id ) {

  if ( empty( $id ) )
    return false;

  $settings = get_option( 'option_tree_settings' );

  if ( empty( $settings['settings'] ) )
    return false;

  foreach( $settings['settings'] as $setting ) {

    if ( $setting['id'] == $id && isset( $setting['label'] ) ) {

      return $setting['label'];

    }
    

  }
  return false;

}
//gen breadcrumb
function breadcrumbs($trangchu,$searchtext='',$showpageparent=false) {
  
  $name = $trangchu; //text for the 'Home' link
  
  if ( !is_home() || !is_front_page() || is_paged() ) {
  
    echo '<ol class="breadcrumb">';
  
    global $post;
    $home = get_bloginfo('url');
    echo '<li><a href="' . $home . '">' . $name . '</a></li>';
  	if($searchtext=='')
	{
		if ( is_category() ) {
		  global $wp_query;
		  $cat_obj = $wp_query->get_queried_object();
		  $thisCat = $cat_obj->term_id;
		  $thisCat = get_category($thisCat);
		  $parentCat = get_category($thisCat->parent);
		  if ($thisCat->parent != 0) {
			echo '<li>';
			echo(get_category_parents($parentCat, TRUE, ''));
			echo '</li>';
		  }
		  echo '<li class="active">';
		  single_cat_title();
		  echo '</li>';
		
		}
		else
		if(is_tag())
		{
			$queried_object = get_queried_object();
			echo '<li>Tag</li>';
			echo '<li>'.$queried_object->name.'</li>';
			echo '</ol>';
		
		}
		else
		if($post->post_type=="page"||$post->post_type=="post")
		{
		  if($post->post_type=="post")
		  {
			$cat = wp_get_object_terms($post->ID,'category');
			$catlink = get_category_link($cat[0]->term_id);
			if($showpageparent) $catlink = str_replace("/category","",$catlink);
			if(!empty($cat)){echo '<li><a href="'.$catlink.'">'.$cat[0]->name.'</a></li>';}
		  }
		  echo '<li class="active">'.$post->post_title.'</li>';
		  echo '</ol>';
			
		}
	}
	else
	{
		echo '<li class="active">Tìm kiếm</li>';
		echo '</ol>';
		
	}
    
  }
}
//thinhnq get excerpt
function gsoft_get_excerpt($post,$num_words,$more)
{
	if ( has_excerpt( $post->ID ) ) {
		$text = get_the_excerpt($post);
	} else {
		$text = strip_tags($post->post_content);
	}
	return wp_trim_words($text,$num_words,$more);
}
//other function
//extend function wordpress
add_filter( 'comments_open', 'my_comments_open', 10, 2 );

function my_comments_open( $open, $post_id ) {

	//$post = get_post( $post_id );

	//if ( 'page' == $post->post_type )
	//	$open = false;
	$open = true;
	return $open;
}

add_action( 'init', 'my_add_excerpts_to_pages' );
function my_add_excerpts_to_pages() {
     add_post_type_support( 'page', 'excerpt' );
}
//extend function wordpress


/*register post type event
add_action( 'init', 'register_postype_event' );
function register_postype_event() {
    $labels = array(
        "name" => __( 'event', 'twentysixteen' ),
        "singular_name" => __( 'event', 'twentysixteen' ),
        "menu_name" => __( 'event', 'twentysixteen' ),
        "all_items" => __( 'Tất cả event', 'twentysixteen' ),
        "add_new" => __( 'Thêm mới event', 'twentysixteen' ),
        "add_new_item" => __( 'Thêm mới event', 'twentysixteen' ),
        "edit_item" => __( 'Chỉnh sửa event', 'twentysixteen' ),
        "new_item" => __( 'Thêm mới event', 'twentysixteen' ),
        "view_item" => __( 'Xem event ', 'twentysixteen' ),
        "search_items" => __( 'Tìm kiếm event', 'twentysixteen' ),
        "not_found" => __( 'Không tìm thấy event', 'twentysixteen' ),
        "not_found_in_trash" => __( 'Không tìm thấy event trong thùng rác', 'twentysixteen' ),
        "parent_item_colon" => __( 'event cha:', 'twentysixteen' ),
        "featured_image" => __( 'Hình ảnh đại diện event', 'twentysixteen' ),
        "set_featured_image" => __( 'Thiết lập hình ảnh đại diện event', 'twentysixteen' ),
        "remove_featured_image" => __( 'Xóa  hình ảnh đại diện event', 'twentysixteen' ),
        "use_featured_image" => __( 'Sử dụng hình ảnh đại diện event', 'twentysixteen' ),
        "archives" => __( 'Lưu trữ event', 'twentysixteen' ),
        "insert_into_item" => __( 'Thêm vào event', 'twentysixteen' ),
        "uploaded_to_this_item" => __( 'Tải lên event', 'twentysixteen' ),
        "filter_items_list" => __( 'Lọc danh sách event', 'twentysixteen' ),
        "items_list_navigation" => __( 'Điều hướng danh sách event', 'twentysixteen' ),
        "items_list" => __( 'Danh sách event', 'twentysixteen' ),
        "parent_item_colon" => __( 'event cha:', 'twentysixteen' ),
        );

    $args = array(
        "label" => __( 'event', 'twentysixteen' ),
        "labels" => $labels,
        "description" => "event",
        "public" => true,
        "publicly_queryable" => true,
        "show_ui" => true,
        "show_in_rest" => false,
        "rest_base" => "",
        "has_archive" => false,
        "show_in_menu" => true,
        "show_in_nav_menus"=>true,
                "exclude_from_search" => false,
        "capability_type" => "post",
        "map_meta_cap" => true,
        "hierarchical" => false,
        "rewrite" => array( "slug" => "event", "with_front" => true ),
        "query_var" => true,
        "menu_position" => 5,"menu_icon" => "".get_bloginfo('url')."/wp-content/uploads/2017/04/calendar.png",
        "supports" => array( "title", "editor" ),        
            );
    register_post_type( "event", $args );
}

*/
function array_sort($array, $on, $order=SORT_ASC)
{
    $new_array = array();
    $sortable_array = array();

    if (count($array) > 0) {
        foreach ($array as $k => $v) {
            if (is_array($v)) {
                foreach ($v as $k2 => $v2) {
                    if ($k2 == $on) {
                        $sortable_array[$k] = $v2;
                    }
                }
            } else {
                $sortable_array[$k] = $v;
            }
        }

        switch ($order) {
            case SORT_ASC:
                asort($sortable_array);
            break;
            case SORT_DESC:
                arsort($sortable_array);
            break;
        }

        foreach ($sortable_array as $k => $v) {
            $new_array[$k] = $array[$k];
        }
    }

    return $new_array;
}
remove_action( 'wpcf7_enqueue_scripts', 'wpcf7_recaptcha_enqueue_scripts' );
add_action( 'wpcf7_enqueue_scripts', 'wpcf7_recaptcha_enqueue_scripts_custom' );
 
function wpcf7_recaptcha_enqueue_scripts_custom() {
    $hl = 'vi';
     
    $url = 'https://www.google.com/recaptcha/api.js';
    $url = add_query_arg( array(
        'hl' => $hl,
        'onload' => 'recaptchaCallback',
        'render' => 'explicit' ), $url );
 
    wp_register_script( 'google-recaptcha', $url, array(), '2.0', true );
}
function fb_move_admin_bar() {
    
}
// on backend area
add_action( 'admin_head', 'fb_move_admin_bar' );
// on frontend area
add_action( 'wp_head', 'fb_move_admin_bar' );