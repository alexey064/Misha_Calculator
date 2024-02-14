@echo off

set build_dir=%1
set src_dir="%cd%/mathexpr"
set build_type=%2

if not exist %build_dir% (
	mkdir %build_dir%
)
cd %build_dir%
cmake -G "Ninja" %src_dir%
cmake --build . --config %build_type% --