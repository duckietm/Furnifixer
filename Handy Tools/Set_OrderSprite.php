<?php

ini_set('display_errors', true);
ini_set('display_startup_errors', false);

if (! file_exists('furnidata_xml.xml')) {
    throw new RuntimeException('furnidata_xml.xml is does not exist or is not readable');
}

$furnidata = new SimpleXMLElement(file_get_contents('furnidata_xml.xml'));
$translate = new SimpleXMLElement(file_get_contents('furnidata_xml.xml'));
$translate = array_merge_recursive(
    (array) $translate->roomitemtypes,
    (array) $translate->wallitemtypes
);

foreach ($translate['furnitype'] as $furni) {
    $file         = (string) $furni->attributes()['classname'];
    $trans[$file] = [
        $furni->classname,
        $furni->offerid,
    ];
}

foreach ($furnidata->roomitemtypes->furnitype as $furni) {
    $file = (string) $furni->attributes()['classname'];

    if (! isset($trans[$file])) {
        continue;
    }

    list($name, $offerid) = $trans[$file];

    $name = $furni['classname'];
	$id = $furni['id'];
    $furni->offerid = $offerid;
	
	$query1 = "UPDATE `items_base` SET sprite_id = {$id} where public_name ='{$name}';";
	$query2 = "UPDATE `catalog_items` SET offer_id = {$offerid} where catalog_name ='{$name}';";
	
	echo $query1 . '</br>';
	// mysqli_query($connection, $query1) or die("Failed to import");
	echo $query2 . '</br>';
	// mysqli_query($connection, $query2) or die("Failed to import");
}

foreach ($furnidata->wallitemtypes->furnitype as $furni) {
    $file = (string) $furni->attributes()['classname'];

    if (! isset($trans[$file])) {
        continue;
    }

    list($name, $offerid) = $trans[$file];

    $name = $furni['classname'];
	$id = $furni['id'];
    $furni->offerid = $offerid;
	
	$query3 = "UPDATE `items_base` SET sprite_id = {$id} where public_name ='{$name}';";
	$query4 = "UPDATE `catalog_items` SET offer_id = {$offerid} where catalog_name ='{$name}';";
	
	echo $query3 . '</br>';
	// mysqli_query($connection, $query1) or die("Failed to import");
	echo $query4 . '</br>';
	// mysqli_query($connection, $query2) or die("Failed to import");
}

?>