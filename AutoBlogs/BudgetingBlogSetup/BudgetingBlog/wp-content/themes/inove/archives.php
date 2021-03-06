<?php
/*
Template Name: Archives
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
				<?php edit_post_link(__('Edit', 'inove'), '<span class="editpost">', '</span>'); ?>
				<div class="fixed"></div>
			</div>
			<div class="fixed"></div>
		</div>
		<div class="content">
			<?php
				if (function_exists('wp_easyarchives')) {
					wp_easyarchives();
				} else {
					echo '<ul>';
					wp_get_archives('type=monthly&show_post_count=1');
					echo '</ul>';
				}
			?>
		</div>
	</div>

<?php else : ?>
	<div class="errorbox">
		<?php _e('Sorry, no posts matched your criteria.', 'inove'); ?>
	</div>
<?php endif; ?>

<?php include('templates/comments.php'); ?>

<?php get_footer(); ?>
