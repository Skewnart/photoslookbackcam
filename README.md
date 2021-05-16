# PhotosLookBackCam

### Introduction
PhotosLookBackCam is an open project that let you have a free and optimized software to look back easily your studio photos.
You just need the camera manufacturer software to let you save your shots into a pc folder, and PhotosLookBackCam shows it to you automatically !
This software can be set to fullscreen with a black background in order to not blind you when you shoot without light.

### Context menu installation
To help your work, you can add this software into the Windows explorer context menu :
To do so, you just need to add entries into Windows Registry.

Under :
<pre><code>
-- HKEY_CLASSES_ROOT/
   |-- Directory/
      |-- Background/
          |-- shell/
            |-- PhotosLookBackCam
                (par défaut)    "Dossier retour caméra"
                Icon            (exe file location)
                |-- Command
                    (par défaut)    "(exe file location)" "%V"
</code></pre>

### Help keystrokes

|Key|What id does|
|---|:-----------|
|D key|Open photos folder in explorer|
|Enter key|Set fullscreen|
