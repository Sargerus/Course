   M S B A M L   `   `   `  ����
  Course%    Course.Views.EditTeachersWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	      m_EditTeachW  � Title#  �   5   	   ��5      �� 5   
    �� ImageSource$( "/Resources/Images/editteachers.jpg��6    j� Stretch$ UniformToFill=�6F    j� TileMode$
 None=�6^   .��5
      �  � RowDefinitions 5   
   �� 5      $��20 �6   6*   �� 5      6   .+�5   
   o� .@�5      m�  
 m_Save#���  6    m� StaysOpenOnClick . .@�5      m� #���  6   6?   5      m� 5        m_Search#���  6    . .@�5      m�   
m_Students#���  6    m� Command 6A   �� *SearchStudentsCommand+6m   m� 5        
m_Teachers#���  6    6A   �� *SearchTeachersCommand+6m   5      m� 5        m_Tools#���  6   .@�5      m�   m_ChangeLang#���  6   .@�5      m� $��Russian��6    60   �� *ChangeLangRusCommand+6[   m� 5      $��English��6    60   �� *ChangeLangEngCommand+6[   5      5       5!      �� 5$   
   $
��Left=�6   $	��275��60   $��	10,10,0,0q�6=   $��1��6P   $	��Top=�6]   $	��255��6u   .+�5%      ��   	m_Surname#���  6   $
��Left=�6:   $��5,10,0,0q�6U   $	��Top=�6g   $��29��6   $��1��6�   $
��Auto��6�   $��16(�6�   �� 5&      $
��Left=�6-   $
��Auto��6H   $��110,-25,0,0q�6V   $��1��6k    �� TextWrapping$
 Wrap=�6x   $	��Top=�6�   $	��120��6�   ��6   �� *LName+6�   �� 5'        
m_WorkNumb#���  6   $
��Left=�6;   $��5,15,0,0q�6V   $	��Top=�6h   $��29��6�   $��1��6�   $
��Auto��6�   $��16(�6�   �� 5)      $
��Left=�62   $
��Auto��6M   $��110,-25,0,0q�6[   $��1��6p   $
 Wrap=�6}   $	��Top=�6�   $	��120��6�   ��6   �� *
TrudNumber+6�   �� 5*      #���  6   $��
-60,15,0,0q�6^   $��80��6r   $��25��6}   ��6:   �� *BeginSeaCommand+6}   �� 5+       	 m_Clear#��� 	 6   $��110,-25,0,0q�6Z   $��80��6o   $��25��6z   ��69   �� *ClearCommand+6z   �� 5,       

 m_Back#��� 
 6   $
��Left=�6X   $��	70,10,0,0q�6s   $��1��6�   $	��Top=�6�   $	��121��6�   $��41��6�   ��68   �� *BackCommand+6�   50      �� 51   
   $
��Left=�6   $
��Auto��60   $��
270,10,0,0q�6@   $��1��6T   $	��Top=�6a   $
��Auto��6y   .+�52      &   System.Windows.Controls.DataGrid     AutoGenerateColumns . $
��Left=�55      $��	20,10,0,0q�56      $��1��57      $	��Top=�58      $
��Auto��59      $
��Auto��5:      	   CanUserAddRows	 . 
   CanUserDeleteRows
 .    CanUserSortColumns .    
IsReadOnly .  ��54      �� *
mainlist+5>      5@      5A      5B      