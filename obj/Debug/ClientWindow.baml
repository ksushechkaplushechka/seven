   M S B A M L   `   `   `  ����  Autobike    Autobike.ClientWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Клиенты��5   	   $	��450��6   $	��800��6&   ���#1E1E2E � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�64    � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Padding��%e  &	        �� 5   
   " ��6+   .�5	      ��  � �� 5
      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   $	 5q�5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   5      5      .��5      � .+�5   
   �� -   5        $��X��5      $��30��6   $��30��6(   $��Right=�64   $	��Top=�6P   
������ ���#E74C3C
������ $
��Bold"�5   Y   $��0,5,5,0q�5   +   �� 5"   
   -   5        
 ��Name$
 LicenseSearchBox��5"      $	��200��6,   $��30��68   $��	10,10,0,0q�6D   $
��Left=�5#      $	��Top=�6-   
�������$��Введите ВУ��5$   $   .��5%   ^   ��5&      �� 5'      " ��6"   .R�5(      �� ���#3A3A3A���#555555$��1q�6H   $ 5��6\   ._�5)      ��  ��Name$ PART_ContentHost��6'   5*      5+      .��5,       5-      �� 50   
   -   5        $��
Поиск��50      $	��100��6!   $��30��6-   $��
220,10,0,0q�69   $
��Left=�51      $	��Top=�6,   ���#4CAF50
����������#4CAF50$��1q�52   O   #��[  53   5   �� 56   
   ���#2E2E3E$ 10��6&   $��10,50,10,60q�68   $	 10q�6M   ._�57      �� $
��Auto=�6   .��58      &   System.Windows.Controls.DataGrid  -   5          Name$ ClientDataGrid��58         AutoGenerateColumns . $��Stretch=�6N   $��Stretch=�6l      
IsReadOnly . 
������ 
�������   RowBackground �#3A3A3A   AlternatingRowBackground �#2E2E2E   GridLinesVisibility$
 None=�5:      
������    ColumnHeaderStyle 5;      �� 5<      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5=      ��  � �� 5>   "   �� 5?   &    �� 
StartPoint$	 0,0&�6:    �� EndPoint$	 1,0&�6K   .��5@   *   �  � Color$ #4CAF50��67    � Offset$	 0.0Q�6G   � 5A   *   $ #3D8B4E��67   $	 1.0Q�6G   5B   '   5D      �� 5E       � ��
 �����6;   �� 5F       � ��$ 16(�69   �� 5G       � ��$
 Bold"�6;   �� 5H       � ��$ Center=�6K   �� 5I       � ��$ 10q�68   �� 5J       � ��
 ���� 6<   5K      5M      5N      5O      �� 5R   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��Bottom=�6I   $��	10,0,0,10q�6d   .+�5S      �� -   5        $��Добавить��5S      $	��100��6(   $��30��64   $��0,0,10,0q�6@   ���#4CAF50
����������#4CAF50$��1q�6�   #��[  6�   6�   �� 5T      -   5        $ ��Редактировать��5T      $	��100��6-   $��30��69   $��0,0,10,0q�6E   
������ ���#4CAF50���#4CAF50$��1q�6�   #��[  6�   6�   �� 5U      -   5        $��Удалить��5U      $	��100��6'   $��30��63   $��0,0,10,0q�6?   
������ 
���  ��
���  ��$��1q�6�   #��[  6�   6�   �� 5V      -   5        $��Сбросить��5V      $	��100��6(   $��30��64   
�������
�������
�������$��1q�6x   #��[  6�   6�   5W      5X      5Y      