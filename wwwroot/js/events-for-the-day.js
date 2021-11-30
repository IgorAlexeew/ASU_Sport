const app = Vue.createApp({
    data() {
        return {

        }
    }
});
app.component('date-picker', {
    data() {
        return {

            date_string: (new URLSearchParams(window.location.search)).get("date"),
            month_strings: [
                "янв",
                "фев",
                "мар",
                "апр",
                "май",
                "июн",
                "июл",
                "авг",
                "сен",
                "окт",
                "ноя",
                "дек",
            ]
        }
    },
    computed: {
        day() {
            return (new Date(this.date_string)).getDate();
        },
        month() {
            return this.month_strings[(new Date(this.date_string)).getMonth()];
        },
        year() {
            return (new Date(this.date_string)).getFullYear();
        },
        date() {
            return new Date(this.date_string);
        }
    },
    template: `
    <div class="date-picker-block">
        <p class="current-date"><span id="day">{{ day }}</span> <span id="month">{{ month }}</span> <span id="year">{{ year }}</span></p>
        <span class="datepicker-toggle">
            <span class="datepicker-toggle-button"></span>
            <input type="date" class="datepicker-input" v-model="date_string">
        </span>
    </div>
    `
});

app.component('page-info', {
    data() {
        return {
            sport_object: (new URLSearchParams(window.location.search)).get("sport-object")
        }
    },
    template: `
        <div class="date-block">
            <p class="sport-object-name">{{ sport_object }}</p>
            <date-picker></date-picker>
        </div>
    `
});
app.mount("#app");