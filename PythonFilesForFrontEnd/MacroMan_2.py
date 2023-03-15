import subprocess
import os
import sys

directory = """ "C:\\Program Files (x86)\\OpenOffice 4\\program" """
buildCommand = " ./swriter.exe"
buildArgs = "C:\\Users\\lucas\\OneDrive\\Desktop\\MacroManipulation\\AttackCode\\attackerWIN.odt"
os.system('cd {} && start "{}" "{}"'.format(directory, buildCommand, buildArgs))
