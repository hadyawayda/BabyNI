import { Dialog, Transition } from '@headlessui/react'
import { Fragment, useRef, useState } from 'react'
import {
   DateRangePicker,
   DateRangePickerChangeEvent,
   SelectionRange,
} from '@progress/kendo-react-dateinputs'
import '@progress/kendo-theme-default/dist/all.css'
import useDateString from './Hooks/useDateString'
import { DateRange } from './Interfaces/Interfaces'

const DateComponent = ({
   onDateChange,
}: {
   onDateChange: (date: DateRange) => void
}) => {
   const [isOpen, setIsOpen] = useState(false)
   let completeButtonRef = useRef(null)
   const [date, setDate] = useState<SelectionRange>({
      start: new Date(new Date().setDate(new Date().getDate() - 30)),
      end: new Date(),
   })

   function handle30Days() {
      setDate({
         start: new Date(new Date().setDate(new Date().getDate() - 30)),
         end: new Date(),
      })
   }

   function handle15Days() {
      setDate({
         start: new Date(new Date().setDate(new Date().getDate() - 15)),
         end: new Date(),
      })
   }

   function handle7Days() {
      setDate({
         start: new Date(new Date().setDate(new Date().getDate() - 7)),
         end: new Date(),
      })
   }

   function handleYesterday() {
      setDate({
         start: new Date(new Date().setDate(new Date().getDate() - 1)),
         end: new Date(),
      })
   }

   function handleDateChange(e: DateRangePickerChangeEvent) {
      setDate(e.value)
   }

   function handleDateSubmit() {
      onDateChange(useDateString(date))
      setIsOpen(false)
   }

   function handleClose() {
      setIsOpen(false)
   }

   function handleOpen() {
      setIsOpen(true)
   }

   return (
      <div>
         <button
            type="button"
            onClick={handleOpen}
            className="whitespace-nowrap rounded-md bg-gray-600 px-4 py-2 text-sm font-medium text-white hover:bg-orange-600 transition-colors duration-200"
         >
            Set Date Range
         </button>
         <Transition
            appear
            show={isOpen}
            as={Fragment}
         >
            <Dialog
               as="div"
               onClose={handleClose}
               className="relative z-50"
               initialFocus={completeButtonRef}
            >
               <Transition.Child
                  as={Fragment}
                  enter="ease-out duration-300"
                  enterFrom="opacity-0"
                  enterTo="opacity-100"
                  leave="ease-in duration-200"
                  leaveFrom="opacity-100"
                  leaveTo="opacity-0"
               >
                  <div
                     className="fixed inset-0 bg-slate-950/60"
                     aria-hidden="true"
                  />
               </Transition.Child>
               <div className="fixed inset-0 flex w-screen items-center justify-center p-4">
                  <Transition.Child
                     as={Fragment}
                     enter="ease-out duration-300"
                     enterFrom="opacity-0 scale-95"
                     enterTo="opacity-100 scale-100"
                     leave="ease-in duration-200"
                     leaveFrom="opacity-100 scale-100"
                     leaveTo="opacity-0 scale-95"
                  >
                     <Dialog.Panel className="relative mx-auto p-9 rounded-2xl flex flex-col justify-between items-center bg-slate-100 dialog-panel">
                        <div className="flex flex-col justify-start items-center">
                           <Dialog.Title className="text-black mb-6">
                              Select Date Range
                           </Dialog.Title>
                           <DateRangePicker
                              className="k-form"
                              value={date}
                              onChange={handleDateChange}
                           />
                        </div>
                        <div className="flex gap-4 items-end h-full mb-6">
                           <button
                              onClick={handleYesterday}
                              className="text-black py-2 px-4 border rounded-md focus:border-orange-600"
                           >
                              Yesterday
                           </button>
                           <button
                              onClick={handle7Days}
                              className="text-black py-2 px-4 border rounded-md focus:border-orange-600"
                           >
                              7 Days
                           </button>
                           <button
                              onClick={handle15Days}
                              className="text-black py-2 px-4 border rounded-md focus:border-orange-600"
                           >
                              15 Days
                           </button>
                           <button
                              onClick={handle30Days}
                              className="text-black py-2 px-4 border rounded-md focus:border-orange-600"
                              ref={completeButtonRef}
                           >
                              30 Days
                           </button>
                        </div>
                        <button
                           onClick={handleDateSubmit}
                           className="flex shrink-0 w-1/2 items-center h-12 justify-center rounded-3xl text-sm tracking-widest border bg-orange-600 hover:bg-white hover:text-orange-600 hover:border-orange-600 transition-colors duration-500"
                        >
                           SUBMIT
                        </button>
                     </Dialog.Panel>
                  </Transition.Child>
               </div>
            </Dialog>
         </Transition>
      </div>
   )
}

export default DateComponent
