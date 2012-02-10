require 'albacore'

msbuild :build do |msb|
	msb.properties :configuration => :Release
	msb.targets :Clean, :Build
	msb.solution = 'S3Cmd.sln'
end

task :output => :build do |o|
	FileUtils.rmtree 'out'
	FileUtils.mkdir_p 'out'
	FileUtils.cp 'bin/Release/S3Cmd.exe', 'out'
	FileUtils.cp 'bin/Release/S3Cmd.exe.config', 'out'
  FileUtils.cp 'bin/Release/AWSSDK.dll', 'out'
end

zip :zip => :output do |z|
	z.directories_to_zip 'out'
	z.output_file = 'S3Cmd.zip'
end
