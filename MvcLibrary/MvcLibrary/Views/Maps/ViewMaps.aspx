﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ViewMaps
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <a href="http://msdn.microsoft.com/zh-cn/library/dd410597.aspx">出处：在 Visual Studio 中创建一个带单元测试的基本 MVC 项目</a>
    <h2>
        My City Maps</h2>
    Select map:
    <select onclick="GetMap(value);">
        <option value="Seattle">Seattle, WA</option>
        <option value="LasVegas">Las Vegas, NV</option>
        <option value="SaltLake">Salt Lake City, UT</option>
        <option value="Dallas">Dallas, TX</option>
        <option value="Chicago">Chicago, IL</option>
        <option value="NewYork">New York, NY</option>
        <option value="Rio">Rio de Janeiro, Brazil</option>
        <option value="Paris">Paris, France</option>
        <option value="Naples">Naples, Italy</option>
        <option value="Keta">Keta, Ghana</option>
        <option value="Beijing">Beijing, China</option>
        <option value="Sydney">Sydney, Australia</option>
    </select>
    <br />
    <br />
    <div id='earthMap' style="position: relative; width: 400px; height: 400px;">
    </div>
    <script charset="UTF-8" type="text/javascript" src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2&mkt=en-us">
    </script>
    <script type="text/javascript">
        var map = null;
        var mapID = '';

        function GetMap(mapID)
        {
            switch (mapID)
            {
                case 'Seattle':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(47.6, -122.33), 10, 'i', true);
                    break;
                case 'LasVegas':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(36.17, -115.14), 10, 'i', true);
                    break;
                case 'SaltLake':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(40.75, -111.89), 10, 'i', true);
                    break;
                case 'Dallas':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(32.78, -96.8), 10, 'i', true);
                    break;
                case 'Chicago':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(41.88, -87.62), 10, 'i', true);
                    break;
                case 'NewYork':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(40.7, -74), 10, 'i', true);
                    break;
                case 'Rio':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(-22.91, -43.18), 10, 'i', true);
                    break;
                case 'Paris':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(48.87, 2.33), 10, 'i', true);
                    break;
                case 'Naples':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(40.83, 14.25), 10, 'i', true);
                    break;
                case 'Keta':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(5.92, 0.983), 10, 'i', true);
                    break;
                case 'Beijing':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(39.91, 116.39), 10, 'i', true);
                    break;
                case 'Sydney':
                    map = new VEMap('earthMap');
                    map.LoadMap(new VELatLong(-33.86, 151.21), 10, 'i', true);
            }
        }   
    </script>
</asp:Content>
