   M S B A M L   `   `   `  ����G  CVIdeo Editor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:VIdeo_EditorVIdeo_Editor  5   	       VIdeo_Editor.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:VIdeo_Editor     5   	     � Title$  Video Editor��5   	   $	��450��6   $	��800��6+   .��5	      � .+�5
   
   �� -   5         ��Name$ openBtn��5
      $��Select Video��6"   $
��Left=�69   $��
43,318,0,0q�6T   $	��Top=�6h   $��93��6�   6�   �� 5   
   -   5        $ saveBtn��5      $
��Save��6"   $
��Left=�61   $��
448,10,0,0q�6L   $	��Top=�6`   $��45��6x   6�   �� 5   
   -   5        $ cutBtn��5      $��	Cut Video��6!   $
��Left=�65   $��196,371,0,0q�6P   $	��Top=�6e   $��88��6}   6�   r� 5   
   -   5         r�Name$ videoPreview��5      $
��Left=�6-   $	��220��6H   $��	43,54,0,0q�6U   $	��Top=�6h   $	��394��6�   �� 5   
   -   5         ��Name$ pathTxt��5      $
��Left=�6#   $��43,7,0,0q�6>    �� TextWrapping$
 Wrap=�6P   $
��.../��6d   $	��Top=�6p   $	��394��6�   $��23��6�   �� 5   
   -   5        $ mergeBtn��5      $��Merge Video��6#   $
��Left=�69   $��349,371,0,0q�6T   $	��Top=�6i   $��88��6�   �� 5   
   -   5        $ audioBtn��5      $��Insert Audio��6#   $
��Left=�6:   $��
43,371,0,0q�6U   $	��Top=�6i   $��88��6�   �� 5   
   -   5        $ playBtn��5      $
��Play��6"   $
��Left=�61   $��196,318,0,0q�6L   $	��Top=�6a   $��44��6y   6�   �� 5   
   -	   5        $ pauseBtn��5      $��Pause��6#   $
��Left=�63   $��245,318,0,0q�6N   $	��Top=�6c   $��44��6{   6�   �� 5   
   -
   5         ��Name$ timelineSlider��5      $
��Left=�6)   $��
43,283,0,0q�6D   $	��Top=�6X   $	��394��6p    � Minimum$ 0Q�5       � Maximum$	 300Q�6   r� 5   
   -   5        $ video1��5      $
��Left=�6'   $	��114��6B   $��
544,54,0,0q�6O   $	��Top=�6c   $	��220��6{   r� 5   
   -   5        $ video2��5      $
��Left=�6'   $	��114��6B   $��544,235,0,0q�6O   $	��Top=�6d   $	��220��6|   �� 5   
   -   5        $ playV1��5      $
��Play��6!   $
��Left=�60   $��613,183,0,0q�6K   $	��Top=�6`   $��44��6x   6�   �� 5   
   -   5        $ playV2��5      $
��Play��6!   $
��Left=�60   $��613,366,0,0q�6K   $	��Top=�6`   $��44��6x   6�   �� 5   
   -   5        $ pauseV1��5      $��Pause��6"   $
��Left=�62   $��662,183,0,0q�6M   $	��Top=�6b   $��44��6z   6�   �� 5   
   -   5        $ pauseV2��5      $��Pause��6"   $
��Left=�62   $��662,366,0,0q�6M   $	��Top=�6b   $��44��6z   6�   5      5      