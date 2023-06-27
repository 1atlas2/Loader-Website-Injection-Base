<?php
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $text = $_POST['text'];
    file_put_contents('command.php', '<?php echo "' . $text . '"; ?>');
    echo "Validated the Request.";
} else {
    echo "Error: Unvalid Request.";
}
?>
