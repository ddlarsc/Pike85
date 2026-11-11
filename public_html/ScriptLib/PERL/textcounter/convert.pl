$cant_num = $not_count_num = $convert_all = 0;

opendir(DIR,".");
@convert = grep(!/^\./, readdir(DIR));
closedir(DIR);

print "This program will convert counter files to the new version.  It will\n";
print "ask you before converting any file, and will hopefully only convert\n";
print "those files which look like counter data files.  If you want to convert\n";
print "all files in this directory, type 'YES' at the prompt.  Otherwise, to\n";
print "convert as you go, type y or n depending on if you want to convert it.\n";

foreach $file (@convert) {
    if ($file eq 'convert.pl') { next; }
    if (!$convert_all) {
        print "Convert File: $file? [y/n] ";
        $answer = <STDIN>;
        if ($answer =~ /YES/) { 
            $convert_all = 1;
            &convert($file);
        }
        elsif ($answer =~ /^y/i) {
            &convert($file);
        }
        else {
            next;
        }
    }
    else {
        &convert($file);
    }
}

if ($cant_num) {
    print "Could Not Rename the following file(s):\n";
    foreach $cant (@cant) {
        print "    $cant\n";
    }
}

if ($not_count_num) {
    print "The following files were not renamed, as we didn't think it was\n";
    print "    a textcounter data file.  You must rename it by hand.\n";
    foreach $not_count (@not_count) {
        print "    $not_count\n";
    }
}

sub convert {
    $old_file = $new_file = $_[0];
    $new_file =~ s/[^\w]/_/g;

    if ((-s "$old_file") < 30) {
        if(!rename($old_file,$new_file)) {
            push(@cant,$old_file);
            $cant_num++;
        }
    }
    else {
        push(@not_count,$old_file);
        $not_count_num++;
    }
}
