   M S B A M L   `   `   `  ����  Autobike    Autobike.AddAccountWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Add Account��5   	   $	��350��6   $	��400��6*   ���#1E1E2E � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�64    � 	Resources 5         RoundedButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Padding��%e  &	        �� 5   
   " ��6+   .�5	      ��  � �� 5
      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   $	 5q�5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   5      5      .��5      � .+�5   
   �� -   5        $��X��5      $��25��6   $��25��6(   $��Right=�64   $	��Top=�6P   
������ ���#E74C3C
������ $
��Bold"�5   Y   $��0,5,5,0q�5   +   �� 5"   
   $��10q�6   $��Center=�6!   .+�5$      �� $��
Car Number��6   $	��0,5q�6*   
�������67   �� 5%      -   5        
 ��Name$
 CarNumberComboBox��5%       �� DisplayMemberPath$ 
car_number��62    �� SelectedValuePath$ car_id��6Q   ���#3A3A3A
���   �
�������$��1q�5&   R   $	��0,5q�6f   �� 5)      $��	Client ID��6   $	��0,5q�6)   
�������66   �� 5*      -   5        $
 ClientIdComboBox��5*      $ client_name��61   $ 	client_id��6Q   ���#3A3A3A
���   �
�������$��1q�5+   R   $	��0,5q�6f   �� 5.      $��Price��6   $	��0,5q�6%   
�������62   �� 5/      -   5         ��Name$ PriceTextBox��5/      ���#3A3A3A
�������
�������$��1q�6g   $	��0,5q�6{   �� 52      $��Rental Start Date��6   $	��0,5q�61   
�������6>   (  "System.Windows.Controls.DatePicker  53      -   5          Name$ RentalStartDatePicker��53      ���#3A3A3A
���   �
�������$��1q�6s   $	��0,5q�6�   �� 56      $��Rental End Date��6   $	��0,5q�6/   
�������6<     57      -   5        $ RentalEndDatePicker��57      ���#3A3A3A
���   �
�������$��1q�6q   $	��0,5q�6�   �� 5:      -   5        $	��Add��5:      $��5q�6;   ���#4CAF50
����������#4CAF50$��1q�6�   $��40��6�   #��[  6�   5;      5<      5=      