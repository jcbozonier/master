<?php
/*
Template Name: Links
*/
?>

<?php get_header(); ?>

<?php if (have_posts()) : the_post(); update_post_caches($posts); ?>

	<div class="post" id="post-<?php the_ID(); ?>">
		<h2><?php the_title(); ?></h2>
		<div class="info">
			<span class="date"><?php the_modified_time(__('F jS, Y', 'inove')); ?></span>
			<div class="act">
				<?php if ($comments || comments_open()) : ?>
					<span class="comments"><a href="#comments"><?php _e('Goto comments', 'inove'); ?></a></span>
					<span class="addcomment"><a href="#respond"><?php _e('Leave a comment', 'inove'); ?></a></span>
				<?php endif; ?>
				<?php if ( $user_ID ) : ?>
					<span class="editlinks"><a href="<?php echo get_settings('siteurl'); ?>/wp-admin/link-manager.php"><?php _e('Edit links', 'inove'); ?></a></span>
				<?php endif; ?>
				<?php edit_post_link(__('Edit', 'inove'), '<span class="editpost">', '</span>'); ?>
				<div class="fixed"></div>
			</div>
			<div class="fixed"></div>
		</div>
		<div class="content">
			<div class="boxcaption"><h3>Blogroll</h3></div>
			<div class="box linkcat">
				<ul><?php wp_list_bookmarks('title_li=&categorize=0&orderby=rand'); ?></ul>
				<div class="fixed"></div>
			</div>

			<?php the_content(); ?>
			<div class="fixed"></div>
		</div>
	</div>

<?php else : ?>
	<div class="errorbox">
		<?php _e('Sorry, no posts matched your criteria.', 'inove'); ?>
	</div>
<?php endif; ?>

<?php include('templates/comments.php'); ?>

<?php get_footer(); ?>
