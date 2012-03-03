<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoundedTableTest.aspx.cs"
    Inherits="_RoundedTable_RoundedTableTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rounded Table Test</title>
    <link href="roundedTable.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="RoundedTable" cellpadding="0" style="width: 100%; height: 100%">
            <tr>
                <td colspan="2" rowspan="2" style="width: 10px;" class="RoundedTableDarkHeaderTL">
                </td>
                <td class="RoundedTableOuterBorder" style="height: 1px;">
                </td>
                <td colspan="2" rowspan="2" class="RoundedTableDarkHeaderTR">
                </td>
            </tr>
            <tr>
                <td class="RoundedTableDarkHeader">
                    如果不写字，header就看不到
                </td>
            </tr>
            <tr>
                <td class="RoundedTableOuterBorder" style="width: 1px;">
                </td>
                <td class="RoundedTableProjectInfoBackground" style="width: 9px;">
                </td>
                <td class="RoundedTableProjectInfoBackground" style="width: 100%;">
                    Sample row
                </td>
                <td class="RoundedTableProjectInfoBackground" style="width: 9px;">
                </td>
                <td class="RoundedTableOuterBorder" style="width: 1px;">
                </td>
            </tr>
            <tr>
                <!--显示边框-->
                <td class="RoundedTableOuterBorder" style="width: 1px;">
                </td>
                <!--用于显示倒角-->
                <td class="RoundedTableLightHeader" style="width: 9px;">
                </td>
                <td class="RoundedTableLightHeader" style="height: 28px; width: 100%;">
                    <table>
                        <tr>
                            <td>
                                这里是一个table，用来安放toolbar
                            </td>
                        </tr>
                    </table>
                </td>
                <!--用于显示倒角-->
                <td class="RoundedTableLightHeader" style="width: 9px;">
                </td>
                <!--显示边框-->
                <td class="RoundedTableOuterBorder" style="width: 1px;">
                </td>
            </tr>
            <tr valign="top" style="height: 100px; width: 100px">
                <td class="RoundedTableOuterBorder" style="width: 1px;">
                </td>
                <td class="RoundedTableDarkInfo" colspan="3" height="100%">
                    <div style="width: 100%; height: 200px;">
                        Content
                    </div>
                </td>
                <td class="RoundedTableOuterBorder" style="width: 1px;">
                </td>
            </tr>
            <tr>
                <td class="RoundedTableDarkFooterBL" colspan="2" rowspan="2">
                    <!--占位，否则显示异常-->
                    <div style="height: 10px; width: 10px" />
                </td>
                <td class="RoundedTableDarkInfo" style="height: 9px;">
                </td>
                <td class="RoundedTableDarkFooterBR" colspan="2" rowspan="2">
                    <div style="height: 10px; width: 10px" />
                </td>
            </tr>
            <tr>
                <td class="RoundedTableOuterBorder" style="height: 1px;">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
