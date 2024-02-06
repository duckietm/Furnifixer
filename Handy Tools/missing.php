<?php


$connect = mysqli_connect("####DB IP / LOCALHOST####", "### USERNAME ###", "### PASSWORD ###", "### DATABASE ###");

if (!$connect) {
    echo "Error: Unable to connect to MySQL." . PHP_EOL;
    exit;
}


$data = file_get_contents("furnidata.txt");
$data = str_replace("\n", "", $data);
$data = str_replace("[[", "[", $data);
$data = str_replace("]]", "]", $data);
$data = str_replace("][", "],[", $data);


foreach (explode('],[', $data) as $val)
{
    $val = str_replace('[', '', $val);
    $val = str_replace(']', '', $val);
    
    $bits = explode(',', $val);
    $name = str_replace('"', '', $bits[2]);
    
    $stufftoupdate[] = '[' . $val . ']';
}

foreach ($stufftoupdate as $stuff)
    {
        #Start select item_name
        $stuff = str_replace('"s",', '', $stuff);
        $stuff = str_replace('"i",', '', $stuff);
        $furni = explode('[', $stuff);
        $furni = explode(']', $furni[1]);
        $nome = explode('","', $furni[0]);
        $nome = explode('","', $nome[1]);
        $nome = $nome[0];
        #End select item_name
        
        #Start select sprite_id
        $stuff = str_replace('"s",', '', $stuff);
        $stuff = str_replace('"i",', '', $stuff);
        $furni = explode('[', $stuff);
        $furni = explode(']', $furni[1]);
        $id = explode('"', $furni[0]);
        $id = explode('","', $id[1]);
        $id = $id[0];
        #End select sprite_id
        
        
        $idfurni = mysqli_query($connect, "SELECT * FROM items_base WHERE item_name = '$nome'");
        $furni = mysqli_fetch_array($idfurni);
        $furniid = $furni['id'];
        $update = mysqli_query($connect, "SELECT * FROM items_base WHERE sprite_id = ".$id."");
		$resultarr = mysqli_fetch_assoc($update);
        if ($resultarr==FALSE) echo("".$nome."<br/>");
        }
	mysqli_close($connect);
?> 