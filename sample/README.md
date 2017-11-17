FreshEssentials for Xamarin.Forms has ONLY the most common elements you need for Xamarin.Forms. It's contains the elements you need in almost every project and nothing more, things like BindablePicker, SegementedButtons, InverseBooleanConverter, TappedGestureAttached, ListViewItemTappedAttached and not much more. It's the lightweight essentials. 

### Configuration
**iOS**

To init Fresh Essentials on iOS, add the following line to your AppDelegate.cs:

	new FreshEssentials.iOS.AdvancedFrameRendereriOS();

### Controls
**BindablePicker**

BindablePicker inherits from Xamarin.Forms.Picker, you can binding data to ItemSource as Items, and also can set which property you want to display via DisplayProperty. 

If you want to use it in XAML, you need to include the namespace first 

	xmlns:fe="clr-namespace:FreshEssentials;assembly=FreshEssentials"
	
Then 

	<fe:BindablePicker ItemsSource="{Binding MyCars}" SelectedItem="{Binding SelectedCar}" DisplayProperty="MakeAndModel" Title="Select..." />

**AdvancedFrame (flexible rounded corners)**

AdvancedFrame inherits from Frame, you can set corner type via Corners(There are only four type, left, right, all, none), you can also set CornerRadius and InnerBackground color 

	<fe:AdvancedFrame Corners="left" CornerRadius="10" InnerBackground="Blue" OutlineColor="Red" >
		<Label Text="Corners is left, CornerRadius is 10, InnerBackground is Blue" TextColor="White"/>
	</fe:AdvancedFrame>
	
**SegmentedButtonGroup**

SegmentedButtonGroup is like iOS Segmented Controls, you can binding SelectedIndex for it

	<fe:SegmentedButtonGroup OnColor="Blue" OffColor="White" SelectedIndex="{Binding SelectIndex, Mode=TwoWay}">
		<fe:SegmentedButtonGroup.LabelStyle>
			<Style TargetType="Label">
			    <Setter Property="FontSize" Value="12" />
				<Setter Property="FontAttributes" Value="Bold" />
			</Style>
		</fe:SegmentedButtonGroup.LabelStyle>
		<fe:SegmentedButtonGroup.SegmentedButtons>
			<fe:SegmentedButton Title="Button 1"/>
			<fe:SegmentedButton Title="Button 2"/>
			<fe:SegmentedButton Title="Button 3"/>
		</fe:SegmentedButtonGroup.SegmentedButtons>
	</fe:SegmentedButtonGroup>

###### This is the component, works on iOS, Android and UWP soon.
![](https://raw.githubusercontent.com/XAM-Consulting/FreshEssentialsSample/master/SegmentedButtonGroupiOS.png)

### Converters

**InverseBooleanConverter**

Used for binding inversed bool value 

	<ContentPage.Resources>
		<ResourceDictionary>
			 <fe:InverseBooleanConverter x:Key="InverseConverter" />
		</ResourceDictionary>
	</ContentPage.Resources>

	<Button Text="Click Me" IsVisible="{Binding ShowButton, Converter={StaticResource InverseConverter}}" />
	
### Attached Properties

**ListViewItemTappedAttached**

Used for binding item tapped command to a ListView

	<ListView ItemsSource="{Binding MyCars}" fe:ListViewItemTappedAttached.Command="{Binding ItemTapCommand}">
	
**TappedGestureAttached**

Used for binding a tapped command to any VisualElement.

	<Image Source="xamconsulting.png" fe:TappedGestureAttached.Command="{Binding ImageTappedCommnad}" />
