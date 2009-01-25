<?php
/*
Plugin Name: Smart Ads
Plugin URI: http://www.johnkolbert.com/portfolio/wp-plugins/smart-ads
Description: Automatically adds advertisements before and after your post's single page content based on word count or post age. You can also ad custom ads using the built in shortcode. Just type [smartads] into your posts to display the custom ad.
Author: John Kolbert
Version: 2.0
Author URI: http://www.johnkolbert.com/hire/

Copyright Notice

Copyright © 2008-2009 by John Kolbert

Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the “Software”), to deal in 
the Software without restriction, including without limitation the rights to use, 
copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the 
Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

register_activation_hook( __FILE__, 'smartads_init' );

function smartads_init(){
	$options = get_option('smart_ads_insert');
	
	if(!$options){ //set defaults
		$options['smart_ads_showindexcust'] = 0;
		$options['smart_ads_custcomment'] = 'html';
	    $options['smart_ads_wordcount'] = 0;
    	$options['smart_ads_days'] = 0;
	    $options['smart_ads_catex'] = '';
    	$options['smart_ads_regsuser'] = '';
	    $options['smart_ads_imgex'] = '';
    	$options['smart_ads_imgchar'] = 0;
   	 update_option('smart_ads_insert', $options);
   	}
}

add_action('admin_notices', 'smartads_admin_warning');

function smartads_check_legacy(){
//checks if the user is upgrading from a previous version where the "yourads.php" file was used
	
	$options = get_option('smart_ads_insert');
	
	//if this file is there it means you've upgraded from the old plugin
	$oldfile = ABSPATH . 'wp-content/plugins/' . dirname(plugin_basename(__FILE__)) . '/yourads.php';
	
	if($oldfile && !($options['topad'] || $options['bottomad'] || $options['customad1'])){ 
		return true;  //means the old file is there AND the new ads aren't set
	}
}


function smartads_admin_warning(){
//displays the admin warning if the legacy file is found

	if(smartads_check_legacy()){
		echo "<div id='smartads-warning' class='updated fade'><p><strong>".__('SmartAds Needs Your Attention.')."</strong> ".sprintf(__('You must <a href="%1$s">update your Ad Code</a> for your advertisements to work properly.'), "options-general.php?page=".plugin_basename(__FILE__))."</p></div>";
	}
}

add_action('admin_menu', 'smartads_option_menu');

function smartads_option_menu() {  
// install the options menu
	if (function_exists('current_user_can')) {
		if (!current_user_can('manage_options')) return;
	} else {
		global $user_level;
		get_currentuserinfo();
		if ($user_level < 8) return;
	}
	if (function_exists('add_options_page')) {
		add_options_page(__('Smart Ads'), __('Smart Ads'), 1, __FILE__, 'smartads_options_page');
	}
} 


function smartads_options_page(){
//displays the options page

	if (isset($_POST['update_options'])) {
	$options['smart_ads_showindexcust'] = trim($_POST['smart_ads_showindexcust'],'{}');
	$options['smart_ads_custcomment'] = trim($_POST['smart_ads_custcomment'],'{}');
	$options['legacy_enabled'] = trim($_POST['legacy_enabled'],'{}');
    $options['smart_ads_wordcount'] = trim($_POST['smart_ads_wordcount'],'{}');
    $options['smart_ads_days'] = trim($_POST['smart_ads_days'],'{}');
    $options['smart_ads_catex'] = trim($_POST['smart_ads_catex'],'{}');
    $options['smart_ads_regsuser'] = trim($_POST['smart_ads_regsuser'],'{}');
    $options['smart_ads_imgex'] = trim($_POST['smart_ads_imgex'],'{}');
    $options['smart_ads_imgchar'] = trim($_POST['smart_ads_imgchar'],'{}');
    $options['topad'] = stripslashes(trim($_POST['topad'],'{}'));
    $options['bottomad'] = stripslashes(trim($_POST['bottomad'],'{}'));
    $options['customad1'] = stripslashes(trim($_POST['customad1'],'{}'));
    update_option('smart_ads_insert', $options);
		// Show a message to say we've done something
		echo '<div class="updated"><p>' . __('Options saved') . '</p></div>';
	} else {
		
		$options = get_option('smart_ads_insert');
	}
	 
	?>
		<div class="wrap">
		<h2><?php echo __('Smart Ads Options Page'); ?></h2>
    <p>Created by <a href="http://www.johnkolbert.com/hire/">John Kolbert</a><br />
    Main page and support: <a href="http://www.johnkolbert.com/portfolio/wp-plugins/smart-ads">Official listing</a></p>
    <p>Smart Ads is a plugin that helps you monitize your blog by automatically placing ads before and after your post content. These ads only show up on the single page view. You can control when these ads show up by wordcount or post date. You can also place a custom ad anywhere you like in your post!</p>
		
				<?php if(smartads_check_legacy()){ 
					include('yourads.php');
					$options['topad'] = $topad;
					$options['bottomad'] = $bottomad;
					$options['customad1'] = $customad1;
				?>
			<p class='updated fade'>I've noticed that you're upgrading from the previous version of Smart Ads. Smart Ads now lets you enter your ads directly into the text boxes at the bottom of this page (actually, it requires it).
			Currently your ads are found in the Smart Ads plugin folder as <code>yourads.php</code>. For your piece of mind I've tried to copy their values into the forms below for you. Should it fail,
			you'll need to paste them yourself. Once you press update, this message will dissapear.</p>
			<?php } ?>

		
		
		<form method="post" action="">
		
		<table class="form-table">
		   <tr valign="top">
		    <th scope="row">Show Custom ads on Homepage?</th>
		    <td><input type="checkbox" name="smart_ads_showindexcust" id="smart_ads_showindexcust" <?php if ($options['smart_ads_showindexcust'] == 1) {echo "checked";} ?> value="1"  /> Yes <br /> If selected, Custom ads will be displayed on the main blog page. Otherwise they will only be displayed on the single post or page view.<br /><strong>Note</strong>: <a href="https://www.google.com/adsense/support/bin/answer.py?answer=48182&sourceid=aso&subid=ww-ww-et-asui&medium=link&gsessionid=DERvyq807mw">Google policies state</a> that only three ad units may be displayed on a single page, so be careful using this option.</td>
		  </tr>
		  
      <tr valign="top">
				<th scope="row">Minimum Post <br /> Word Count:</th>
					<td><input name="smart_ads_wordcount" type="text" id="smart_ads_wordcount" value="<?php echo $options['smart_ads_wordcount']; ?>" size="5" /> (enter 0 to disable); Suggested Value: 200<br />Top and Bottom ads will not be displayed unless the post word count is more then this setting.<br />This does not affect Custom Ads.</td>
			</tr>
			<tr valign="top">
				<th scope="row">Only show ads on posts older then:</th>
					<td><input name="smart_ads_days" type="text" id="smart_ads_days" value="<?php echo $options['smart_ads_days']; ?>" size="5" /> <big> days</big> (enter 0 to disable); Suggested Value: 30<br /> This regulates the automatic insertion of Top and Bottom Ads, but does not affect the Custom Ads.</td>
			</tr>
			<tr valign="top">
			  <th scope="row">Category Exclusions:</th>
			    <td><input name="smart_ads_catex" type="text" id="smart_ads_catex" value="<?php echo $options['smart_ads_catex']; ?>" size="10" /> List category IDs seperated by a comma and a single space (such as 15, 2, 19)<br />These categories will not have ads placed in them. This does not affect Custom Ads.</td>
			</tr>
			<tr valign="top">
			  <th scope="row">Disable Top Ad if Post Begins with an Image:</th>
		    <td><input type="checkbox" name="smart_ads_imgex" id="smart_ads_imgex" <?php if ($options['smart_ads_imgex'] == 1) {echo "checked";} ?> value="1"  > Yes, if an image tag is found within the first 
            <input name="smart_ads_imgchar" type="text" id="smart_ads_imgchar" value="<?php echo $options['smart_ads_imgchar']; ?>" size="3" /> characters (including HTML characters). Recommended value: 200<br /> This does not affect the Bottom Ad or Custom Ads.</td>
			</tr>
			<tr valign="top">
			  <th scope="row">Disable Ads For Registered Members:</th>
		    <td><input type="checkbox" name="smart_ads_regsuser" id="smart_ads_regsuser" <?php if ($options['smart_ads_regsuser'] == 1) {echo "checked";} ?> value="1"  > Yes <br /> If selected, registered members of your blog will not see any ads, even custom ads.</td>
			</tr>
		   <tr valign="top">
				<th scope="row">Enable Legacy custom ads notation:</th>
					<td><input type="checkbox" name="legacy_enabled" id="legacy_enabled" <?php if ($options['legacy_enabled'] == "yes") {echo "checked";} ?> value="yes"  /> Yes (not recommended)<br />NOTE: Smart Ads uses WordPress' built in shortcode notation to display custom ads. Simply type <code>[smartads]</code> where you want your custom ad to appear. However, those upgrading from previous versions may want to continue using the older method, you can enable it below.
					 <br /><strong>Notation style:</strong><br /><input name="smart_ads_custcomment" type="radio" id="smart_ads_custcomment" value="comment" <?php if (($options['smart_ads_custcomment'] !== "bracket") ) {echo 'checked';} ?> /> <?php $data = '<!-smartads->'; echo htmlentities($data); ?> (an HTML comment)<br />
         <input name="smart_ads_custcomment" type="radio" id="smart_ads_custcomment" value="bracket" <?php if ($options['smart_ads_custcomment'] == "bracket") {echo 'checked';} ?> />{smartads}<br /> It's recommended to use the official shortcode notation: [smartads]
    	</tr>
      </table>
		
		<div class="postbox">
		<h3>Advertising Code</h3>
		<div class="inside">

		<table class="form-table">
      		<tr valign="top">
				<th scope="row">Top Ad:<br /><small>This ad will appear before your post content</small></th>
				<td><textarea name="topad" id="topad" cols="50%" rows="6" ><?php echo stripslashes($options['topad']); ?></textarea></td>
			</tr>
			<tr valign="top">
			  <th scope="row">Bottom Ad:<br /><small>This ad will appear after your post content</small></th>
				<td><textarea name="bottomad" id="bottomad" cols="50%" rows="6" ><?php echo stripslashes($options['bottomad']); ?></textarea></td>
		  </tr>
		  <tr valign="top">
		    <th scope="row">Custom Ad:<br /><small>This ad is inserted using shortcodes. Simply type <code>[smartads]</code> in your post where you want it to appear</small></th>
				<td><textarea name="customad1" id="customad1" cols="50%" rows="6" ><?php echo stripslashes($options['customad1']); ?></textarea></td>
		  </tr>  
		</table>
		</div></div>
	</fieldset>
		<div class="submit"><input type="submit" name="update_options" value="<?php _e('Update') ?>"  style="font-weight:bold;" /> </div>
		</form> 
    	<p>Now that you are making mega-bucks using this free plugin, consider <a href="http://www.johnkolbert.com/portfolio/wp-plugins/smart-ads">donating any amount</a> to help continue development.</p>
	</div>
	<?php	
}

function smartads_write_box() {
//displays the "disable ads for this post" box
    global $post;
    $options = get_option('smart_ads_insert');

	if (function_exists('current_user_can')) {
		if (!current_user_can('manage_options')) return;
	} else {
		global $user_level;
		get_currentuserinfo();
		if ($user_level < 8) return;
	}       
     $disable = get_post_meta($post->ID, 'smartadsdisable', $single = true);  // 1 = disabled
	if ($disable == 1) $disable = "checked";
	echo '<p><input type="checkbox" name="smart_ads_disable" id="smart_ads_disable "' .$disable . ' value="1"  ></td><td style="font-size: 11px;">Disable ads for this post.<br /><small> This does not affect Custom Ads.</small></p>';

}

add_action('admin_menu', 'smartads_add_post_boxes');

function smartads_add_post_boxes(){
//adds the "disable ads" post box
	add_meta_box('smartads_write_box', 'Smart Ads', 'smartads_write_box', 'post', 'side', 'low');
}

add_action('edit_post', 'add_smartads_custom_field');
add_action('publish_post', 'add_smartads_custom_field');
add_action('save_post', 'add_smartads_custom_field');

function add_smartads_custom_field($id){
//adds the post meta_key for disabled ads

  $smartdisable = $_POST["smart_ads_disable"];
    
  if (get_post_meta($id, 'smartadsdisable', $single = true) === $smartdisable){
	return;
  }
  else{
     delete_post_meta($id, 'smartadsdisable');
     if($smartdisable) add_post_meta($id, 'smartadsdisable', $smartdisable);
    } 
  }
  

function smartads_wordcount($content){     
//Count number of words in post
  
  $wordcount = count(explode(" ", strip_tags(trim($content))));
  return $wordcount;
}
  
  
function smartads_catex(){   
//check post category against excluded category list

  $options = get_option('smart_ads_insert');
 
  $catex = trim($options['smart_ads_catex']);
  $catex = explode(", ", $catex);  
  
  for( $i=0; $i < count($catex); ++$i)
        {
        if(in_category($catex[$i]))
            return 1; 
        }
   return;
}


function smartads_imgexclude($content){       
//exclude posts that start with images, pretty crude function right now
  $options = get_option('smart_ads_insert');
  
  if(($options['smart_ads_imgex'] == 1) && (stristr(substr($content, 0, $options['smart_ads_imgchar']), '<img') == true)){
     return 1;
     }
 
  return;
}

function smartads_legacy_replace($content, $customad){
//enables the legacy replacement from previous version
  $options = get_option('smart_ads_insert');
  if($options['legacy_enabled'] == "yes"){
  	if ($options['smart_ads_custcomment'] == "bracket"){
      	$custtype = '{smartads}';
    }
  	else{
      	$custtype = '<!-smartads->';
  	}
  	
  	$replace = stripslashes($customad);
    $content = ereg_replace($custtype, $replace, $content);
    return $content;
  }
  return $content;		
}

//game time

add_filter('the_content', 'smartads_insert', 9);

function smartads_insert($content){  
//adds the ads
if(!is_feed()){
  global $post;
  $options = get_option('smart_ads_insert');
  $topad = stripslashes($options['topad']);
  $bottomad = stripslashes($options['bottomad']);
  $customad = stripslashes($options['customad1']);
  
  get_currentuserinfo();
  global $user_level;
  
if (($user_level > 0) && ($options['smart_ads_regsuser'] == 1 )) {  //if the user is logged in and we want to hide the ads
   return $content;
}

else {  
  if (is_page()) {
        $content = smartads_legacy_replace($content, $customad);
        return $content;
      }
  
  if (is_home()) {

      if ($options['smart_ads_showindexcust'] == 1){
        $content = smartads_legacy_replace($content, $customad);
        return $content;
      }
      else {
        $content = smartads_legacy_replace($content, $customad = "");
        return $content;
        }
    }
  
  if (is_single()){ 
   if ($post->post_date < date('Y-m-d H:i:s', strtotime("-".$options[smart_ads_days] . "days"))) {
    
    if ((smartads_wordcount($content) >= $options['smart_ads_wordcount']) && (get_post_meta($post->ID, 'smartadsdisable', $single = true) != 1) && (!smartads_catex()) ){
        $replace = stripslashes($customad);
        
        $content = smartads_legacy_replace($content, $customad);
        
        if (smartads_imgexclude($content) == 1) {
          $content = $content . $bottomad;
          return $content;
        }
        else {
        $content = $topad . $content . $bottomad;
        return $content;
        }
    }     
    else {
         $content = smartads_legacy_replace($content, $customad);
        return $content;
      }
    }    
  else{
    $content = smartads_legacy_replace($content, $customad);
    return $content;
    }
   }
   else {
   	return $content;
   }    
  }
 }
 return $content; //if it's the RSS, just return the content
}


function smartads_shortcode() {
  $options = get_option('smart_ads_insert');
  get_currentuserinfo();
  global $user_level;
  
	if (($user_level > 0) && ($options['smart_ads_regsuser'] == 1 )) {  //if the user is logged in and we want to hide the ads
   		return;
	}

	if (is_single() || (is_home() && $options['smart_ads_showindexcust'] == 1)){ //return the replacement if were in an RSS feed
		return $options['customad1'];
	}
	else{
		return;
	}
}

add_shortcode('smartads', 'smartads_shortcode'); //install the shortcode
 
  
  
  
  
?>