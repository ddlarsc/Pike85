//**************************************
    
    

    <?
    $countfile = "counter.txt"; // Check, File Exist or not, for Count storage    	

    	
    if (file_exists($countfile)) { 			
    	$fp = fopen($countfile, "r"); 			// Open the file
    	 $output = fread($fp, filesize($countfile)); 	// Read and get the count
    	fclose($fp);					// Close the file
    	
    	$count = intval($output);			// Make sure the value is a number
    } 
    else { 						// No file was found
    	$count = 0; 					
    }
    // This is the function used to add and 
    //     then display the count on your page
    function ShowCount() { 
    	global $ShowCount, $countfile, $count; 	
    	
    	$count++; 				// Increment 1
    	
    	$fp = fopen($countfile, "w"); 		
    	 fwrite($fp, $count); 			// Save the new count
    	fclose($fp); 				// Close the file
    	 
    	$ShowCount = $count; 			
    	return $ShowCount; 			
    }
    ?>
