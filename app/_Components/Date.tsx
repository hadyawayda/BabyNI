import { Dialog, Transition } from '@headlessui/react'
import { Fragment, useState } from 'react'
import { DateRangePicker } from '@progress/kendo-react-dateinputs'
import '@progress/kendo-theme-default/dist/all.css'
import { DateProps } from '../Interfaces/Interfaces'
import useCurrentDate from './useCurrentDate'

const DateComponent = ({ onDateChange }: DateProps) => {
   const [startDate, setStartDate] = useState(useCurrentDate()[0])
   const [endDate, setEndDate] = useState(useCurrentDate()[1])
   const [isOpen, setIsOpen] = useState(false)
   const defaultValue = { start: null, end: new Date() }

   onDateChange(startDate)

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
            className="rounded-md bg-gray-600 px-4 py-2 text-sm font-medium text-white hover:bg-orange-600 transition-colors duration-200"
         >
            Set Date Range
         </button>
         <Transition
            appear
            show={isOpen}
            as={Fragment}
         >
            <Dialog
               onClose={handleClose}
               className="relative z-50"
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
                  ></div>
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
                              defaultShow={true}
                              className="k-form"
                              defaultValue={defaultValue}
                           />
                        </div>
                        <button
                           onClick={handleClose}
                           className="flex w-1/2 items-center h-12 justify-center rounded-3xl text-sm tracking-widest border bg-orange-600 hover:bg-white hover:text-orange-600 hover:border-orange-600 transition-colors duration-500"
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
