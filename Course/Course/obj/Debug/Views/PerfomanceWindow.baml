   M S B A M L   `   `   `  ����
  Course#    Course.Views.PerfomanceWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	      m_Perfomance  � Title#  �   5   	   ��5      �� 5   
    �� ImageSource$&  /Resources/Images/perfomance.jpg��6    j� Stretch$ UniformToFill=�6D    j� TileMode$
 None=�6\   .��5	      �  � RowDefinitions 5
   
   �� 5      $��20 �6   6*   �� 5      6   .+�5   
   o� .@�5      m�  
 m_Save#���  6    m� StaysOpenOnClick . .@�5      m� #���  6   6?   5      m� 5        m_Search#���  6    . .@�5      m�   
m_Students#���  6F    m� Command 6   �� *SearchStudentsCommand+6n   m� 5        
m_Teachers#���  6E    6   �� *SearchTeachersCommand+6m   5      m� 5        m_Tools#���  6   .@�5      m�   m_ChangeLang#���  6   .@�5      m� $��Russian��6    60   �� *ChangeLangRusCommand+6[   m� 5      $��English��6    60   �� *ChangeLangEngCommand+6[   5      5      5      �� 5!   
   #���  6   $
��Left=�69   $	��Top=�6T   $	��150��6l   $��50��6x   $��1��6�   $��	10,10,0,0q�6�   ��6�   �� *ShowTeachersCommand+6�   �� 5"   
   #���  6   $
��Left=�69   $	��Top=�6T   $	��150��6l   $��50��6x   $��1��6�   $��	10,65,0,0q�6�   ��6�   �� *ShowStudentCommand+6�   &   System.Windows.Controls.DataGrid  5$   
      AutoGenerateColumns . $
��Left=�6Q   $��
236,10,0,0q�6l   $��1��6�   $	��Top=�6�   $
��Auto��6�   $
��Auto��6�   ��6.   �� *
Perfomance+6�   5%      5&      