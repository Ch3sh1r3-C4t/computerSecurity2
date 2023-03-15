import subprocess
import os
import sys

directory = """ "C:\\Program Files (x86)\\OpenOffice 4\\program" """
buildCommand = " ./swriter.exe"
buildArgs = "C:\\Users\\lucas\\OneDrive\\Desktop\\ContentManipulation\\TrustedDocument\\original_trusted_document_signed.odt"
os.system('cd {} && start "{}" "{}"'.format(directory, buildCommand, buildArgs))
