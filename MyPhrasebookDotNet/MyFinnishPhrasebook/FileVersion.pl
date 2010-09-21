
Main();

sub Main()
{
	# Read file content
	my $fileName = $ARGV[0];
	open ( F, $fileName ) || die "Can't open $fileName: $!";
	my @lines = <F>;
	close( F );

	# Read date vars
	($sec,$min,$hour,$mday,$mon,$year,$wday,$yday,$isdst) = localtime(time);
	$year += 1900;
	$mon += 1;			
	my $monDay = GetTwoDigitNumber($mon) . GetTwoDigitNumber($mday);
			
	# Open file for writing
	open ( F, "> $fileName" ) || die "Can't open $fileName: $!";

	# Write the file back and update relevant date lines
	for ( my $i = 0; $i < @lines; $i++ )
	{
		my $line = $lines[$i];
		chomp( $line );
		
		# Seek for the format:
		# MAJOR_VERSION.YYYY.MMDD.COUNTER
		if ( $line =~ /(.*\")(\d+)\.(\d+)\.(\d\d)(\d\d)\.(\d+)(\".*)/ )
		{
			# Disassemble version components
			my $pre = $1;
			my ($vMajor, $vYear, $vMon, $vDay, $vCounter) = ($2, $3, $4, $5, $6);
			my $post = $7;
			
			# Check if a new day has arrived
			if ( $year > $vYear || $mon > $vMon || $mday > $vDay )
			{
				print "$year > $vYear || $mon > $vMon || $mday > $vDay\n";
				$vCounter = 0;
			}
			
			my $version = "$vMajor.$year.$monDay." . ($vCounter + 1);

			print "Version = $version\n";
			$line = $pre . $version . $post;

		}
		
		print F "$line\n";
	}
	close( F );
	
	print "Done.\n";
}

sub GetTwoDigitNumber
{
	my ($num) = @_;
	if ( $num < 10 )
	{
		return "0$num";
	}
	
	return "$num";
}