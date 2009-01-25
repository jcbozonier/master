=== Plugin Name ===
Contributors: 
Donate link: https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=daniele%2elippi%40gmail%2ecom&item_name=wp%2dstatpress&no_shipping=0&no_note=1&tax=0&currency_code=EUR&lc=IT&bn=PP%2dDonationsBF&charset=UTF%2d8
Tags: stats, statistics, widget, admin, sidebar, visits, visitors, pageview, user, agent, referrer,post,posts,spy,statistiche
Requires at least: 2.0.2
Tested up to: 2.5
Stable Tag: 1.2.9.2

The real-time plugin dedicated to the management of statistics about blog visits. It collects information about visitors, spiders, search keywords, feeds, browsers etc.


== Description ==

The real-time plugin dedicated to the management of statistics about blog visits. It collects information about visitors, spiders, search keywords, feeds, browsers etc.

Once the plugin StatPress has been activated it immediately starts to collect statistics information.
Using StatPress you could spy your visitors while they are surfing your blog or check which are the preferred pages, posts and categories.
In the Dashboard menu you will find the StatPress page where you could look up the statistics (overview or detailed).
StatPress also includes a widget one can possibly add to a sidebar (or easy PHP code if you can't use widgets!).

= Multilanguage =
StatPress speaks English, Italian, Spanish, French, German, Russian, Norwegian, Dutch, Brazilian, Turkish, Swedish!
Could you translate StatPress in your language? Thank you!
( please post to http://www.irisco.it/forums/forum.php?id=1 )

= What's new? =
Check "Other notes" tab to find out updates history!

= Ban IP =

You could ban IP list from stats editing def/banips.dat file.

= DB Table maintenance =

StatPress can automatically delete older records to allow the insertion of newer records when limited space is present.

= StatPress Widget / StatPress_Print function =

Widget is customizable. These are the available variables:

* %thistotalvisits% - this page, total visits
* %since% - Date of the first hit
* %visits% - Today visits
* %totalvisits% - Total visits
* %os% - Operative system
* %browser% - Browser
* %ip% - IP address
* %visitorsonline% - Counts all online visitors
* %usersonline% - Counts logged online visitors
* %toppost% - The most viewed Post
* %topbrowser% - The most used Browser
* %topos% - The most used O.S.

Now you could add these values everywhere! StatPress >=0.7.6 offers a new PHP function *StatPress_Print()*.
* i.e. StatPress_Print("%totalvisits% total visits.");


== Installation ==

Upload "wp-statpress" directory in wp-content/plugins/ . Then just activate it on your plugin management page.
You are ready!!!


= Update =

* Deactivate StatPress plugin (no data lost!)
* Override "wp-statpress" directory in wp-content/plugins/
* Re-activate it on your plugin management page
* In the Dashboard click "StatPress", then "StatPressUpdate" and wait until it will add/update db's content

== Frequently Asked Questions ==

= I've a problem. Where can I get help? =

Please post your messages to <a href="http://www.irisco.it/forums/forum.php?id=1">statpress forum</a>

= Is it wp_2.3.x compatible? =

Of course!


== Screenshots ==
&middot; <a href="http://www.irisco.it/wp-content/uploads/overview.jpg">screenshot-1 - Overview</a><br>
&middot; <a href="http://www.irisco.it/wp-content/uploads/details.jpg">screenshot-2 - Details</a><br>
&middot; <a href="http://www.irisco.it/wp-content/uploads/widget.jpg">screenshot-3 - Widget</a><br>
&middot; <a href="http://www.irisco.it/wp-content/uploads/spy.jpg">screenshot-4 - Spy</a><br>
&middot; <a href="http://www.irisco.it/wp-content/uploads/search.jpg">screenshot-5 - Search</a><br>


== Updates ==


*Update from 0.1 to 0.2*

* Layout update
* iPhone and other new defs

*Update from 0.2 to 0.3 (15 Jul 2007)*

* Rss Feeds support!
* Layout update
* New defs

*Update from 0.3 to 0.4 (14 Sep 2007)*

* Customizable widget
* New defs

*Update from 0.4 to 0.5 (25 Sep 2007)*

* New "Overview"
* New defs

*Update from 0.5 to 0.5.2 (3 Oct 2007)*

* Solved (rare) compatibility issues - Thanks to Carlo A.

*Update from 0.5.2 to 0.5.3 (4 Oct 2007)*

* Solved setup compatibility issues - Thanks to Andrew

*Update from 0.5.3 to 0.6 (17 Oct 2007)*

* New interface layout
* Export to CSV
* MySQL table size in Overview

*Update from 0.6 to 0.7 (22 Oct 2007)*

* Unique visitors
* New graphs (and screenshots)

*Update from 0.7 to 0.7.1 (27 Oct 2007)*

* (one time) Automatically database table creation

*Update from 0.7.1 to 0.7.2 (30 Oct 2007)*

* Now "Last Pages" and "Pages" sections don't count spiders hits - Thanks to Maddler
* Page title decoded
* New spider defs - Thanks to Maddler

*Update from 0.7.2 to 0.7.3 (8 Nov 2007)*

* New IP banning (new file def/banips.dat)
* Functions updated, bugs resolved - Thanks to Maddler
* New "Overview"
* Updated defs

*Update from 0.7.3 to 0.7.4 (12 Nov 2007)*

* New Spy section
* Updated defs

*Update from 0.7.4 to 0.7.5 (14 Nov 2007)*

* New gfx look
* Updated defs

*Update from 0.7.5 to 0.7.6 (25 Nov 2007)*

* New SEARCH section!
* New StatPress_Print() function

*Update from 0.7.6 to 0.7.7 (28 Nov 2007)*

* New SEARCH section!
* New Options panel
* (Optionally) StatPress collects data about logged users
* New Widget variables: VISITORSONLINE and USERSONLINE

*Update from 0.7.7 to 0.8 (3 Dec 2007)*

* "Automatically delete visits older than..." option

*Update from 0.8 to 0.9 (10 Dec 2007)*

* Added search by IP
* New IP lookup service: hostip.info (spy with flags!)
* New spiders
* "Support" link in dashboard

*Update from 0.8 to 0.9.5 (16 Dec 2007)*

* Multilanguage (support English and Italian... could you help me?)
* Spy links fixed
* Update Overview graph with optional num.of days
* Update queries slashed

*Update from 0.9.5 to 0.9.6 (17 Dec 2007)*

* Spanish translation (Thanks to nv1962)

*Version 1.0*

* WP Date and Time settings support (UTC + timezone offset)

*Version 1.1*

* Time settings patch

*Version 1.2*

* French translation (Thanks to Joel Remaud)
* German translation (Thanks to Martin Bartels)
* Russian translation (Thanks to Aleksandr)
* Portuguese/Brazilian translation (Thanks to gmcosta)
* New option: Minimum "capability" to view stats
* Some Overview update
* 20071225: Dutch translation (Thanks to Matthijs www.hethaagseblog.nl)

*Version 1.2.1*

* Norwegian translation (Thanks to Selveste Radiohode)

*Version 1.2.2 (2 Jan 2008)*

* Resolved some bugs

*Version 1.2.3 (16 Jan 2008)*

* Two Turkish translation (Thanks to Singlemen http://berkant.info/blog/?p=618 and Resat)
* Swedish translation (Thanks to Bjšrn Felten)
* Path independent (Thanks to Christian Heim)
* Some new widget variables
* Some fixes

*Version 1.2.4 (10 Feb 2008)*

* New widget: TopPosts
* "Overview" optimization (Thanks to nexia)
* Security patch (Thanks to livibetter)
* .def updates

*Version 1.2.5 (14 Feb 2008)*

* New option "Do not collect spiders visits"
* Compatibility issue: remove jdmonthname() func
* New Last spiders table
* Selectable fields delimiter in CSV export

*Version 1.2.6 (20 Feb 2008)*

* TopPosts widget new href links (Thanks Flip and Frank)
* .def updates (Thanks to GT)
* Interface fixes

*Version 1.2.7 (27 Feb 2008)*

* New menu layout (top level)
* Updated TopPosts widget code (Thanks to crashdumpfr)
* New hook "send_headers"
* Js, plugins and themes doesn't count
* New spiders (Thanks to M66B)

*Version 1.2.8 (27 Feb 2008)*

* Some Feed issues resolved

*Version 1.2.9 (8 Mar 2008)*

* Feed issues resolved (Thanks to Frank http://www.webtlk.com )
* Comment Feeds support

*Version 1.2.9.1 (16 Apr 2008)*

* Search works again!
* defs updated (Thanks to all forum users!)

*Version 1.2.9.2 (23 Jun 2008)*

* XSS vulnerability patch (Thanks to rogeriopvl blog.rogeriopvl.com)

