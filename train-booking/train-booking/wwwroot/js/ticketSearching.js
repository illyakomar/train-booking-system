$(document).ready(function () {
    $('#searching-button').on('click', function () {
        $('#tickets-table tr').filter(function () {
            const departurePointValue = $('#departure-point').val();
            const destinationValue = $('#destination').val();
            const departurePointDateValue = $('#departure-point-date').val();
            const tds = $(this).find('td');
            const tdsTextForSearch = [
                tds.eq(0).text().toLowerCase(),
                tds.eq(1).text().toLowerCase(),
                tds.eq(3).text().split(' ')[0]
            ];
            if (!departurePointValue || !destinationValue || !departurePointDateValue) {
                $(this).toggle(false);
                return;
            }

            const splitedRouteDate = departurePointDateValue.split('-');
            const formatedDeparturePointDateValue = `${splitedRouteDate[2]}.${splitedRouteDate[1]}.${splitedRouteDate[0]}`;

            if (departurePointValue.toLowerCase() !== tdsTextForSearch[0]
                || destinationValue.toLocaleLowerCase() !== tdsTextForSearch[1]
                || formatedDeparturePointDateValue.split(' ')[0] !== tdsTextForSearch[2]) {
                $(this).toggle(false);
                return;
            }
            $(this).toggle(true);
        });
    });
});